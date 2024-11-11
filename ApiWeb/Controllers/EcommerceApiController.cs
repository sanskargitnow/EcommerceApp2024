using ApiWeb.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Services.Data;
using Services.Migrations;
using Services.Models;
using System.Runtime.Intrinsics.Arm;
using ShoppingCartDto = ApiWeb.DTOs.ShoppingCartDto;

namespace ApiWeb.Controllers
{
    [ApiController]

    public class EcommerceApiController : ControllerBase
    {
        private readonly ApplicationDbContext _Dbcontext;

        public EcommerceApiController(ApplicationDbContext context)
        {
            _Dbcontext = context;
        }

        [HttpGet]
        [Route("api/getAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ApiDto>>> GetCategories()
        {
            var categories = await _Dbcontext.Category.ToListAsync();

            // Map Category entities to CategoryDTO
            var categoryDTOs = categories.Select(category => new ApiDto
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder
            }).ToList();

            return Ok(categoryDTOs);
        }


        [HttpGet]
        [Route("api/getCategory/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiDto>> GetCategory(int id)
        {

            var category = await _Dbcontext.Category.FirstOrDefaultAsync(u => u.Id == id);

            // If the category doesn't exist, return a 404 Not Found
            if (category == null)
            {
                return NotFound();
            }

            // Map the Category entity to CategoryDTO
            var categoryDTO = new ApiDto
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder
            };

            // Return the DTO with 200 OK
            return Ok(categoryDTO);
        }


        [HttpPost]
        [Route("api/createCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateCategory(CreateCategoryDtos obj)
        {

            if (obj == null)
                return NotFound();

            //var ob = new CreateCategoryDtos
            //{
            //    Id = obj.Id,
            //    Name = obj.Name,
            //    DisplayOrder = obj.DisplayOrder,
            //};


            var category = new Category
            {
                Name = obj.Name,
                DisplayOrder = obj.DisplayOrder

            };

            _Dbcontext.Category.Add(category);
            await _Dbcontext.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete]
        [Route("api/deleteCategory/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // For successful delete
        [ProducesResponseType(StatusCodes.Status404NotFound)]  // If category not found
        public async Task<IActionResult> DeleteCategory(int id)
        {
            // Find the category entity in the database
            var category = await _Dbcontext.Category.FirstOrDefaultAsync(u => u.Id == id);

            // If no category found with the provided ID, return 404
            if (category == null)
            {
                return NotFound(new { message = "Category not found." });
            }

            // Remove the category from the DbSet
            _Dbcontext.Category.Remove(category);

            // Save changes to the database
            await _Dbcontext.SaveChangesAsync();

            // Return NoContent (204) as the operation is successful
            return NoContent();
        }


        ///Product api


        [HttpGet]
        [Route("api/getAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)] // For successful delete
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var Productresponse = await _Dbcontext.Products.ToListAsync();

            var productDTO = Productresponse.Select(product => new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ISBN = product.ISBN,
                Author = product.Author,
                ListPrice = product.ListPrice,
                Price = product.Price,
                Price50 = product.Price50,
                Price100 = product.Price100,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
            }).ToList();

            return Ok(productDTO);
        }





        [HttpGet("{id}")]
        [Route("api/getProductsByID/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // For successful delete
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            // Fetch the product by Id from the database
            var product = await _Dbcontext.Products.FindAsync(id);

            // Check if the product exists
            if (product == null)
            {
                return NotFound();
            }

            // Map the product entity to ProductDto
            var productDto = new Products
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ISBN = product.ISBN,
                Author = product.Author,
                ListPrice = product.ListPrice,
                Price = product.Price,
                Price50 = product.Price50,
                Price100 = product.Price100,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl
            };

            // Return the product details
            return Ok(productDto);
        }





        [HttpPost]
        [Route("api/addToCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddToCart(ShoppingCartDto obj)
        {
            ;



            if (obj == null)
                return NotFound();

            //var ob = new CreateCategoryDtos
            //{
            //    Id = obj.Id,
            //    Name = obj.Name,
            //    DisplayOrder = obj.DisplayOrder,
            //};

            ShoppingCart ob = await _Dbcontext.shoppingCarts.FindAsync(obj.ProductId);

            if (ob != null)
            {

                ob.Count += obj.Count;
                var newobj = new ShoppingCart
                {
                    Id = ob.Id,
                    Count = ob.Count,
                    ProductId = ob.ProductId,
                    Product = ob.Product,
                    Price = ob.Price,


                };

                _Dbcontext.shoppingCarts.Add(newobj);
            }
            else
            {
                var newobj = new ShoppingCart
                {
                    Id = obj.Id,
                    Count = obj.Count,
                    ProductId = obj.ProductId,
                    Product = obj.Product,
                    Price = obj.Price,


                };

                _Dbcontext.shoppingCarts.Add(newobj);
            }



            await _Dbcontext.SaveChangesAsync();

            return NoContent();
        }





        [HttpGet]
        [Route("api/CartView")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Index()
        {




            var shoppingCartVM = await _Dbcontext.shoppingCarts.ToListAsync();



            var shoppingC = new ShoppingCartVM()
            {
                ShoppingCartList = shoppingCartVM,
                OrderHeader = new()
            };

            //var shoppingListDTOs = shoppingCartVM.Select(obj => new ShoppingCart
            //{
            //    Id = obj.Id,
            //    ProductId = obj.ProductId,
            //    Product = obj.Product,
            //    Price = obj.Price,
            //    Count = obj.Count,
            //}).ToList();

            foreach (var cart in shoppingC.ShoppingCartList)
            {
                var temp = await _Dbcontext.Products.FindAsync(cart.ProductId);
                cart.Price = getPriceBasedOnQuantity(cart, temp);
                shoppingC.OrderHeader.OrderTotal += (cart.Price * cart.Count);

            }

            return Ok(shoppingC);






        }

        private double getPriceBasedOnQuantity(ShoppingCart shoppingcart, Products product)
        
        
        
         
        {
            if (shoppingcart.Count <= 50)
            {
                return product.Price;
            }
            else
            {

                if (shoppingcart.Count <= 100)
                {
                    return product.Price50;
                }
                else
                {

                    return product.Price100;


                }
            }
        }




        [HttpPost]
        [Route("api/plusToCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PlusToCart([FromBody] int cartId)
        {
            if (cartId == 0)
                return NotFound(new { message = "Cart ID is invalid." });

            // Find the cart by ID
            ShoppingCart ob = await _Dbcontext.shoppingCarts.FindAsync(cartId);

            if (ob != null)
            {
                // Increment the count directly on the existing object
                ob.Count = ob.Count + 1;

                // Update the existing cart (no need to create a new object)
                 _Dbcontext.shoppingCarts.Update(ob);
                await _Dbcontext.SaveChangesAsync();

                return Ok(new { message = "Product count updated successfully." });
            }

            return NotFound(new { message = "Cart not found." });
        }







        [HttpPost]
        [Route("api/MinusToCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> MinusToCart([FromBody] int cartId)
        {
            if (cartId == 0)
                return NotFound(new { message = "Cart ID is invalid." });

            // Find the cart by ID
            ShoppingCart ob = await _Dbcontext.shoppingCarts.FindAsync(cartId);

            if (ob != null)
            {
                // Increment the count directly on the existing object

                if (ob.Count == 1)
                {
                    _Dbcontext.Remove(ob);

                }
                else
                {
                    ob.Count = ob.Count - 1;
                    _Dbcontext.shoppingCarts.Update(ob);
                }

                // Update the existing cart (no need to create a new object)
                
                await _Dbcontext.SaveChangesAsync();

                return Ok(new { message = "Product count updated successfully." });
            }

            return NotFound(new { message = "Cart not found." });
        }






        [HttpPost]
        [Route("api/DeleteToCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteToCart([FromBody] int cartId)
        {
            if (cartId == 0)
                return NotFound(new { message = "Cart ID is invalid." });

            // Find the cart by ID
            ShoppingCart ob = await _Dbcontext.shoppingCarts.FindAsync(cartId);

            if (ob != null)
            {
                // Increment the count directly on the existing object

                _Dbcontext.shoppingCarts.Remove(ob);
            

                // Update the existing cart (no need to create a new object)

                await _Dbcontext.SaveChangesAsync();

                return Ok(new { message = "Product deleted successfully." });
            }

            return NotFound(new { message = "Cart not found." });
        }


        [HttpPost]
        [Route("api/SummaryPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Summarypost([FromBody] int cartId)
        {
            if (cartId == 0)
                return NotFound(new { message = "Cart ID is invalid." });

            // Find the cart by ID
            ShoppingCart ob = await _Dbcontext.shoppingCarts.FindAsync(cartId);

            if (ob != null)
            {
                // Increment the count directly on the existing object
                ob.Count = ob.Count + 1;

                // Update the existing cart (no need to create a new object)
                _Dbcontext.shoppingCarts.Update(ob);
                await _Dbcontext.SaveChangesAsync();

                return Ok(new { message = "Product count updated successfully." });
            }

            return NotFound(new { message = "Cart not found." });
        }












    }

}

