using ECommerceWeb.ApiService;
using ECommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ApiWeb.DTOs;


namespace ECommerceWeb.Controllers
{



        public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiServices _productService;

        public HomeController(ILogger<HomeController> logger , ApiServices productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await _productService.GetProductsAsync();


            return View(productList);
        }


        public async Task<IActionResult> Details(int productId)
        {

            ShoppingCartDto obj = new ShoppingCartDto()
            {
                Product = await _productService.GetProductByIdAsync(productId),
                Count = 1,
                ProductId = productId,

            };


            return View(obj);
        }



        public async Task<IActionResult> AddToCart (ShoppingCartDto obj)
        {
            var res = await _productService.AddtoCart(obj);

            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
