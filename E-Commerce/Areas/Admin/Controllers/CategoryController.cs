using E_Commerce.DataAccess.Data;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
   [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWorkContext;
        public CategoryController(IUnitOfWork db)
        {
            _UnitOfWorkContext = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _UnitOfWorkContext.Category.GetAll().ToList();
            return View(objCategoryList);

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "the displayorder cannot exactly match the name .");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWorkContext.Category.Add(obj);
                _UnitOfWorkContext.save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();

        }
        public IActionResult Edit(int? id)
        {
            Console.WriteLine("kkk", id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _UnitOfWorkContext.Category.Get(u => u.Id == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id ==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _UnitOfWorkContext.Category.update(obj);
                _UnitOfWorkContext.save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();

        }


        public IActionResult Delete(int? id)
        {
            Console.WriteLine("kkk", id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _UnitOfWorkContext.Category.Get(u => u.Id == id);



            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _UnitOfWorkContext.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _UnitOfWorkContext.Category.Remove(obj);
            _UnitOfWorkContext.save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");




        }
    }
}
