using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;
using Ecommerce.Models.ViewModels;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWorkContext;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork db , IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWorkContext = db;
            _webHostEnvironment = webHostEnvironment;        }
        public IActionResult Index()
        {
            List<Products> objCategoryList = _UnitOfWorkContext.Product.GetAll(includeProperties:"Category").ToList();

           
            return View(objCategoryList);

        }


        

        public IActionResult Upsert(int ? id)
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
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _UnitOfWorkContext.Product.Get(u => u.Id == id);
                return View(productVM);

            }

          
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj , IFormFile ? file)
        {

             
          
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName) ;
                    string productName = Path.Combine(wwwRootPath, @"images\product\");

                    if(!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productName + fileName) ,FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    };

                    obj.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if(obj.Product.Id == 0)
                {
                    _UnitOfWorkContext.Product.Add(obj.Product);
                }
                else
                {
                    _UnitOfWorkContext.Product.update(obj.Product);
                }
             
                _UnitOfWorkContext.save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                obj.CategoryList = _UnitOfWorkContext.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(obj);

            }


        }
        //public IActionResult Edit(int? id)
        //{
        //    Console.WriteLine("kkk", id);
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Products? ProductFromDb = _UnitOfWorkContext.Product.Get(u => u.Id == id);
        //    //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
        //    //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id ==id).FirstOrDefault();

        //    if (ProductFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ProductFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Products obj)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _UnitOfWorkContext.Product.update(obj);
        //        _UnitOfWorkContext.save();
        //        TempData["success"] = "Product updated successfully";
        //        return RedirectToAction("Index", "Product");
        //    }

        //    return View();

        //}


        //public IActionResult Delete(int? id)
        //{
        //    Console.WriteLine("kkk", id);
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Products ? ProductFromDb = _UnitOfWorkContext.Product.Get(u => u.Id == id);



        //    if (ProductFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ProductFromDb);
        //}
        //[HttpPost, ActionName("delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Products ? obj = _UnitOfWorkContext.Product.Get(u => u.Id == id);
        //    if(obj == null)
        //    {
        //        return NotFound();
        //    }

        //    _UnitOfWorkContext.Product.Remove(obj);
        //    _UnitOfWorkContext.save();
        //    TempData["success"] = "Product deleted successfully";
        //    return RedirectToAction("Index", "Product");




        //}

        /* #apiCall*/

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Products> objCategoryList = _UnitOfWorkContext.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = objCategoryList   });
        }




        [HttpDelete]
        public IActionResult delete(int ? id)
        {
            var productToBeDeleted = _UnitOfWorkContext.Product.Get(u => u.Id == id);

            if(productToBeDeleted == null)
            {
                return Json(new { success = false , message = "Error while deleting" });
            }


            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _UnitOfWorkContext.Product.Remove(productToBeDeleted);
            _UnitOfWorkContext.save();

            return Json(new { success = true, message = "deletion successful" });


        }

        /* #regions */
    }
}
