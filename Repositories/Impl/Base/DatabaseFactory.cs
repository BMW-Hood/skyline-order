using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface IDatabaseFactory
    {
        MySqlDbContext Get(string connectionString);
    }
    public class DatabaseFactory : IDatabaseFactory
    {
        public MySqlDbContext Get(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<MySqlDbContext>();
            builder.UseMySql(connectionString);
            return new MySqlDbContext(builder.Options);
        }
    }
}
