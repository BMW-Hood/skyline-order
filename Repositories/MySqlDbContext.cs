using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}