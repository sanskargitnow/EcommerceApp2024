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
    public class ShoppingCartRepository : Repository<ShoppingCart> , IShoppingCartRepository
    {

        private readonly ApplicationDbcontext _db;

        public ShoppingCartRepository(ApplicationDbcontext db) : base(db) 
        {
            _db = db;
        }

       

       

       

       

        public void update(ShoppingCart obj)
        {
            _db.ShoppingCarts.Update(obj);
        }
    }
}
