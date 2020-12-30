using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eShopWithReact.Services.ProductCatalog.Core.Entities
{
    public class Product : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Product name is a required field.")]
        public string ProductName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "Category is a required field.")]
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Invalid Price")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Price")]
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        [BsonIgnore]
        public Category CategoryDetail { get; set; }
    }
}
