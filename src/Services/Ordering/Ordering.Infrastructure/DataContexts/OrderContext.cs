using eShopWithReact.Services.Ordering.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShopWithReact.Services.Ordering.Infrastructure.DataContexts
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
