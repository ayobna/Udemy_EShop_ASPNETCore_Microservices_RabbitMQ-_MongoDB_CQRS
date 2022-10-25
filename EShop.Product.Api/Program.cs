using EShop.Infrastructure.EventBus;
using EShop.Infrastructure.Mongo;
using EShop.Product.Api.Handlers;
using EShop.Product.DataProvider.Repository;
using EShop.Product.DataProvider.Service;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped< CreateProductHandler>();
RabbitMqOption rabbitMqOption = new RabbitMqOption();
builder.Configuration.GetSection("rabbitmq");
builder.Configuration.Bind(rabbitMqOption);

builder.Services.AddMassTransit(x => {
    x.AddConsumer<CreateProductHandler>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(new Uri(rabbitMqOption.ConnectionString), hostconfig =>
        {
            hostconfig.Username(rabbitMqOption.Username);
            hostconfig.Password(rabbitMqOption.Password);
        });

        cfg.ReceiveEndpoint("create_product", ep => {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(retryConfig => { retryConfig.Interval(2, 100); });
            ep.ConfigureConsumer<CreateProductHandler>(provider);
        });
    }));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


var busControll = app.Services.GetRequiredService<IBusControl>();
busControll.Start();

var dbInitializer = app.Services.GetRequiredService<IDatabaseInitializer>();
dbInitializer.InitializeAsync();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
