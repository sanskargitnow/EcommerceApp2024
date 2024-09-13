using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;
using Ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWorkContext;
        public ProductController(IUnitOfWork db)
        {
            _UnitOfWorkContext = db;
        }
        public IActionResult Index()
        {
            List<Products> objCategoryList = _UnitOfWorkContext.Product.GetAll().ToList();

           
            return View(objCategoryList);

        }

        public IActionResult Create()
        {

            //IEnumerable<SelectListItem> CategoryList = _UnitOfWorkContext.Category.GetAll().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});

           

            ProductVM productVM = new()
            {

                CategoryList = _UnitOfWorkContext.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                Product = new Products()

            };

            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM obj)
        {


          
            if (ModelState.IsValid)
            {
                _UnitOfWorkContext.Product.Add(obj.Product);
                _UnitOfWorkContext.save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
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
            Products? ProductFromDb = _UnitOfWorkContext.Product.Get(u => u.Id == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id ==id).FirstOrDefault();

            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Products obj)
        {

            if (ModelState.IsValid)
            {
                _UnitOfWorkContext.Product.update(obj);
                _UnitOfWorkContext.save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index", "Product");
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
            Products ? ProductFromDb = _UnitOfWorkContext.Product.Get(u => u.Id == id);



            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }
        [HttpPost, ActionName("delete")]
        public IActionResult DeletePost(int? id)
        {
            Products ? obj = _UnitOfWorkContext.Product.Get(u => u.Id == id);
            if(obj == null)
            {
                return NotFound();
            }

            _UnitOfWorkContext.Product.Remove(obj);
            _UnitOfWorkContext.save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index", "Product");




        }
    }
}
