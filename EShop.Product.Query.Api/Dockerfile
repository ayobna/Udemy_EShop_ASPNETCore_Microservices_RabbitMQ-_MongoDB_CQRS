#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EShop.Product.Query.Api/EShop.Product.Query.Api.csproj", "EShop.Product.Query.Api/"]
RUN dotnet restore "EShop.Product.Query.Api/EShop.Product.Query.Api.csproj"
COPY . .
WORKDIR "/src/EShop.Product.Query.Api"
RUN dotnet build "EShop.Product.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShop.Product.Query.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.Product.Query.Api.dll"]