using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace eShopWithReact.Services.ProductCatalog.Core.Entities
{
    public class Category : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Category name is a required field.")]
        public string CategoryName { get; set; }
        public string ImageUrl { get; set;  }
        public string LinkUrl { get; set; }
    }
}
