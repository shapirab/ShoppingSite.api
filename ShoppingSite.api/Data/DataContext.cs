using Microsoft.EntityFrameworkCore;
using ShoppingSite.api.Data.DataModels.Entity;

namespace ShoppingSite.api.Data
{
    public class DataContext : DbContext
    {
        DbSet<UserEntity> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
