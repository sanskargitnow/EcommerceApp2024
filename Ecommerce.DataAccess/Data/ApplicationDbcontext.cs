using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DataAccess.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {
            
        } 
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1 , Name = "Mens" , DisplayOrder = 1},
                 new Category { Id = 2, Name = "Women", DisplayOrder = 2 },
                  new Category { Id = 3, Name = "Electronics", DisplayOrder = 3 });

        }
    }
}
