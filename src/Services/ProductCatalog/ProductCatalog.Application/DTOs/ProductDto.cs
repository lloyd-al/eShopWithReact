using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace eShopWithReact.Services.ProductCatalog.Application.DTOs
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public CategoryDto CategoryDetail { get; set; }
    }
}
