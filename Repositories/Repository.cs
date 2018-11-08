using Models;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public interface IRepository<T> where T : Base
    {
        bool Exist(T t);

        void Add(T t);

        void DeleteById(int id);

        T FindById(int id);

        IEnumerable<T> FindAll();

        IEnumerable<T> FindPage(int pageIndex, int pageSize, out int total);

        void Commit();
    }

    public class Repository<T> : IRepository<T> where T : Base
    {
        protected MySqlDbContext DbContext { get; set; }

        public Repository(MySqlDbContext dbContext)
        {
            DbContext = dbContext;
            dbContext.Database.EnsureCreated();
        }

        public bool Exist(T t)
        {
            throw new NotImplementedException();
        }

        public void Add(T t)
        {
            DbContext.Add(t);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public T FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindPage(int pageIndex, int pageSize, out int total)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}