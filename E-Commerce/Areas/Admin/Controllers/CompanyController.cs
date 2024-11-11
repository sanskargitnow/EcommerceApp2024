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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _UnitOfWorkContext;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork db)
        {
            _UnitOfWorkContext = db;

              }
        public IActionResult Index()
        {
            List<Company> objCategoryList = _UnitOfWorkContext.Company.GetAll().ToList();

           
            return View(objCategoryList);

        }


        

        public IActionResult Upsert(int ? id)
        {

            //IEnumerable<SelectListItem> CategoryList = _UnitOfWorkContext.Category.GetAll().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});

           

          
            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else
            {




                Company company = _UnitOfWorkContext.Company.Get(u => u.Id == id);
                return View(company);

            }

          
        }
        [HttpPost]
        public IActionResult Upsert(Company companyobj)
        {

             
          
            if (ModelState.IsValid)
            {
               

                if(companyobj.Id == 0)
                {
                    _UnitOfWorkContext.Company.Add(companyobj);
                }
                else
                {
                    _UnitOfWorkContext.Company.update(companyobj);
                }
             
                _UnitOfWorkContext.save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index", "Company");
            }
            else
            {
               

                return View(companyobj);

            }


        }
        
        /* #apiCall*/

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _UnitOfWorkContext.Company.GetAll().ToList();

            return Json(new { data = objCompanyList   });
        }




        [HttpDelete]
        public IActionResult delete(int ? id)
        {
            var CompanyToBeDeleted = _UnitOfWorkContext.Company.Get(u => u.Id == id);

            if(CompanyToBeDeleted == null)
            {
                return Json(new { success = false , message = "Error while deleting" });
            }


          

            _UnitOfWorkContext.Company.Remove(CompanyToBeDeleted);
            _UnitOfWorkContext.save();

            return Json(new { success = true, message = "deletion successful" });


        }

        /* #regions */
    }
}
