using E_Commerce.DataAccess.Data;
using Ecommerce.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbcontext _dbcontext;

        internal DbSet<T> dbset;
        public Repository(ApplicationDbcontext db)
        {
            _dbcontext = db;
            this.dbset = _dbcontext.Set<T>();
            _dbcontext.Products.Include(u => u.Category).Include(u => u.CategoryId); 
        }
        public void Add(T entity)
        {
          dbset.Add(entity);
            
        }

        public void Delete(T entity)
        {
           
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter , string? includeProperties = null)
        {
            IQueryable<T> query = dbset;

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var property in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)) {

                    query = query.Include(property);

                    }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string ? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var property in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)) {

                    query = query.Include(property);

                    }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
           dbset.RemoveRange(entity);
        }
    }
}
