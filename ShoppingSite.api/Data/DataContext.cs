using Microsoft.EntityFrameworkCore;
using ShoppingSite.api.Data.DataModels.Entity;

namespace ShoppingSite.api.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
    }
}
