using Microsoft.EntityFrameworkCore;


namespace eShopWithReact.Common.Infrastructure.DataContexts
{
    public abstract class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
