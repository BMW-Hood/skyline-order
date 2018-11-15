using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Common;

namespace Repositories
{


    public class Repository<T> : IRepository<T> where T : Base
    {
        protected DbSet<T> DbSet { get; private set; }
        protected MySqlDbContext dbContext;
        protected IDatabaseFactory _databaseFactory;
        private string connectionString;
        public Repository(IDatabaseFactory databaseFactory,AppSettings settings)
        {
            connectionString = settings.ConnectionString;
            dbContext = DbContext;

        }
        protected MySqlDbContext DbContext => dbContext ??  _databaseFactory.Get(connectionString);

        public bool Exist(T t)
        {
            throw new NotImplementedException();
        }

        public int Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void BulkAddWithCommit(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public T GetById(string id)
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public (int, IList<T>) GetListByPage<TKey>(Expression<Func<T, bool>> where, Func<T, TKey> keySelector, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 10 : pageSize;
            var skip = (pageIndex - 1) * pageSize;
            var query = DbSet.Where(where).OrderByDescending(keySelector);
            var count = query.Count();
            var list = query.Skip(skip).Take(pageSize);
            return (count, list == null ? new List<T>() : list.ToList());
        }

    }
}