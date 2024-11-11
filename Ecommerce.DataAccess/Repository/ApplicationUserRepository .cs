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
    public class ApplicationUserRepository : Repository<ApplicationUser> , IApplicationUserRepository
    {

        private readonly ApplicationDbcontext _db;

        public ApplicationUserRepository (ApplicationDbcontext db) : base(db) 
        {
            _db = db;
        }

       

       

       

       
    }
}
