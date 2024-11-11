using ApiWeb.DTOs;
using ECommerceWeb.ApiService;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ECommerceWeb.Controllers;

    public class ProductController : Controller
    {

    private readonly ApiServices _productService;

    public ProductController(ApiServices productService)
    {
        _productService = productService;

    }

    public async Task<IActionResult> Index()
    {
        var productList = await _productService.GetProductsAsync();


        return View(productList);

    }
}

