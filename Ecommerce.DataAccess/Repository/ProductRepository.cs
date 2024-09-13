using E_Commerce.DataAccess.Data;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class ProductRepository : Repository<Products>, IProductRepository
    {

        private readonly ApplicationDbcontext _db;

        public ProductRepository(ApplicationDbcontext db) : base(db)
        {
            _db = db;
        }







       

        

        public void update(Products obj)
        {
            _db.Products.Update(obj);
        }
    }
}