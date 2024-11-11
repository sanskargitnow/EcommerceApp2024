using ECommerceWeb.ApiService;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ApiServices _productService;

        public CartController(ILogger<CartController> logger, ApiServices productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {

            var CartView = await _productService.GetCartView();


            return View(CartView);
        }


        public IActionResult Summary()
        {
            return View();
        }


        public async Task<IActionResult> Plus(int cartId)
        {
            var plustocart = await _productService.plustocart(cartId);

            if (plustocart)
                return RedirectToAction("Index" , "cart");


            return NotFound();

        }


        public async Task<IActionResult> Minus(int cartId)
        {
            var plustocart = await _productService.minustocart(cartId);

            if (plustocart)
                return RedirectToAction("Index", "Cart");


            return NotFound();

        }


        public async Task<IActionResult> Remove(int cartId)
        {
            var plustocart = await _productService.removetocart(cartId);

            if (plustocart)
                return RedirectToAction("Index", "cart");


            return NotFound();

        }
    }
}
