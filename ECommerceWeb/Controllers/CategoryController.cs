using Microsoft.AspNetCore.Mvc;
using ECommerceWeb.ApiService;
using ApiWeb.DTOs;

namespace ECommerceWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApiServices _categoryService;

        public CategoryController(ApiServices categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int id)
        {
            // Call the service to get the category by ID
            //var category = await _categoryService.GetCategoryById(id);
            var category = await _categoryService.GetCategoriesAsync();



             if (category == null)
            {
                return NotFound(); // Handle not found
            }

            // Pass the category data to the view
            return View(category);
        }


        public async Task<IActionResult> createCategory()
        {

            return View();
        }





        public async Task<IActionResult> ACreateCategory(CreateCategoryDtos obj)
        {

            var res = _categoryService.CreateCategory(obj);

            if(res == null)
            {
                NotFound();
            }

            return RedirectToAction("Index" , "Category");
        }






        public async Task<IActionResult> DeleteCategory()
        {
            
            return View();
        }

        //    var res = await _categoryService.CreateCategory(obj);

        //    if(res == false)
        //    {
        //        return NotFound();
        //    }

        //    return RedirectToAction("Index", "Category");
        //}



        public async Task<IActionResult> ADeleteCategory(int id)
        {

            var res = _categoryService.DeleteCategory(id);

            if (res == null)
            {
                NotFound();
            }

            return RedirectToAction("Index", "Category");
        }





    }
}

