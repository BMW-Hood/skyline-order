using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private MySqlDbContext dbContext;
        private string connectionString;
        public UnitOfWork(IDatabaseFactory databaseFactory, IAppSettings settings)
        {
            connectionString = settings.ConnectionString;
            _databaseFactory = databaseFactory;
            dbContext = DbContext;
        }
        protected MySqlDbContext DbContext => dbContext ?? _databaseFactory.Get(connectionString);
        public int Commit()
        {
            return DbContext.SaveChanges();
        }
        public async Task<int> CommitAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
