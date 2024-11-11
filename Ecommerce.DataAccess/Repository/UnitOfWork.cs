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
        public ICompanyRepository Company
        {
            get; private set;
        }

        public IShoppingCartRepository ShoppingCart
        {
            get; private set;
        }
        public IApplicationUserRepository ApplicationUser
        {
            get; private set;
        }


        public IOrderDetailRepository OrderDetail
        {
            get; private set;
        }

        public IOrderHeaderRepository OrderHeader
        {
            get; private set;
        }


        public UnitOfWork(ApplicationDbcontext db)
        {
            _UnitOfWorkContext = db;
            Category = new CategoryRepository(_UnitOfWorkContext);
            Product = new ProductRepository(_UnitOfWorkContext);
            Company = new CompanyRepository(_UnitOfWorkContext);    
            ShoppingCart = new ShoppingCartRepository(_UnitOfWorkContext);
            ApplicationUser = new ApplicationUserRepository(_UnitOfWorkContext);
            OrderHeader = new OrderHeaderRepository(_UnitOfWorkContext);
            OrderDetail = new OrderDetailRepository(_UnitOfWorkContext);
        }
        

        public void save()
        {
            _UnitOfWorkContext.SaveChanges();
        }
    }
}
