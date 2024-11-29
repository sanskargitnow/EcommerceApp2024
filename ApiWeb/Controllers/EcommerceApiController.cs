using ApiWeb.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Services.Data;
using Services.Models;

namespace ApiWeb.Controllers
{
    [ApiController]
   
    public class EcommerceApiController:ControllerBase
    {
        private readonly ApplicationDbContext _Dbcontext;

        public EcommerceApiController(ApplicationDbContext context)
        {
            _Dbcontext = context;
        }

        [HttpGet]
        [Route("api/getAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task< ActionResult<IEnumerable<ApiDto>>> GetCategories()
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



    }
}
