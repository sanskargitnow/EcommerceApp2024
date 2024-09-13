using E_Commerce.DataAccess.Data;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {

        private readonly ApplicationDbcontext _db;

        public CategoryRepository(ApplicationDbcontext db) : base(db) 
        {
            _db = db;
        }

       

       

       

        public void update(Category category)
        {
            _db.Update(category);
        }
    }
}
