using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;

namespace EShop.Infrastructure.Command.Product
{
    public class CreateProduct
    {
      
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public string ProductId { get; set; }
        [DefaultValue("ProductName DefaultValue")]
        public string ProductName { get; set; }
        [DefaultValue("ProductDescription DefaultValue")]
        public string ProductDescription { get; set; }
        [DefaultValue(20)]
        public float ProductPrice { get; set; }
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid CategoryId { get; set; }
    }
}
