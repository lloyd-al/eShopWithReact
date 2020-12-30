using System;


namespace eShopWithReact.Services.ProductCatalog.Core.Entities
{
    public abstract class BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; } = DateTime.UtcNow;
    }
}
