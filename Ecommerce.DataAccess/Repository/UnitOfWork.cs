using E_Commerce.DataAccess.Data;
using Ecommerce.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbcontext _UnitOfWorkContext;
        public ICategoryRepository Category
        {
            get; private set;
        }
        public IProductRepository Product
        {
            get; private set;
        }
        public UnitOfWork(ApplicationDbcontext db)
        {
            _UnitOfWorkContext = db;
            Category = new CategoryRepository(_UnitOfWorkContext);
            Product = new ProductRepository(_UnitOfWorkContext);
        }
        

        public void save()
        {
            _UnitOfWorkContext.SaveChanges();
        }
    }
}
