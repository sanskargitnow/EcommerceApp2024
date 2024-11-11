using ApiWeb.DTOs;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using static System.Net.WebRequestMethods;
using Microsoft.Identity.Client;
using Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.ApiService
{
    public class ApiServices
    {
        private readonly HttpClient _httpClient;
       

        public ApiServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // Read the BaseUrl from the appsettings.json
            //_apiBaseUrl = configuration["ApiSettings:BaseUrl"];
        }

        public async Task<ApiDto> GetCategoryById(int id)
        {
            var apiUrl = $"/api/getCategory/{id}"; // Full API URL with base URL

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<ApiDto>(jsonData);
                return category;
            }

            return null; // Handle errors or return a default value
        }



        public async Task<bool> CreateCategory(CreateCategoryDtos obj)
        {
            var apiUrl = $"/api/createCategory";

            var jsonData = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true; // Success
            }


            return false;

        }




        public async Task<bool>DeleteCategory(int id)
        {

        
        
            var apiUrl = $"/api/deleteCategory/{id}";

           
            var response = await _httpClient.DeleteAsync(apiUrl);
            return response.IsSuccessStatusCode;

            if (response.IsSuccessStatusCode)
            {
                return true; // Success
            }


            return false;

        }

        public async Task<IEnumerable<ApiDto>> GetCategoriesAsync()
        {
            var apiUrl = "/api/getAllCategories"; // The API endpoint for fetching categories
            var response = await _httpClient.GetAsync(apiUrl);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON data into a list of ApiDto objects
                var categories = JsonConvert.DeserializeObject<IEnumerable<ApiDto>>(jsonData);
                return categories; // Return the list of categories
            }

            return null;
        }




        //products
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var apiUrl = "/api/getAllProducts"; // The API endpoint for fetching categories
            var response = await _httpClient.GetAsync(apiUrl);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON data into a list of ApiDto objects
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(jsonData);
                return products; // Return the list of categories
            }

            return null;
        }


        public async Task<Products> GetProductByIdAsync(int productId)
        {
            // The API endpoint for fetching a product by its ID
            var apiUrl = $"/api/getProductsByID/{productId}";
            var response = await _httpClient.GetAsync(apiUrl);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON data into a ProductDto object
                var product = JsonConvert.DeserializeObject<Products>(jsonData);
                return product; // Return the product details
            }

            return null; // Return null if the request was not successful
        }







        public async Task<bool> AddtoCart(ShoppingCartDto obj)
        {

            
            var apiUrl = $"/api/addToCart";

            var jsonData = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true; // Success
            }


            return false;

        }

        public async Task<ShoppingCartVM> GetCartView()
        
        {
            var apiUrl = "/api/CartView"; // The API endpoint for fetching categories
            var response = await _httpClient.GetAsync(apiUrl);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON data into a list of ApiDto objects
                var CartView = JsonConvert.DeserializeObject<ShoppingCartVM>(jsonData);
              
                return CartView; // Return the list of categories
            }

            return null;
        }



        public async Task<bool> plustocart(int cartId)
        {
            var apiUrl = "/api/plusToCart"; // The API endpoint for updating the cart

            // Convert cartId to JSON and wrap it in StringContent
            var jsonData = JsonConvert.SerializeObject(cartId);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
           

            // Post the content to the API
            var response = await _httpClient.PostAsync(apiUrl, content);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                return true; ; // Success
            }

            return false; // Return null if the request was not successful
        }







        public async Task<bool> minustocart(int cartId)
        {
            var apiUrl = "/api/MinusToCart"; // The API endpoint for updating the cart

            // Convert cartId to JSON and wrap it in StringContent
            var jsonData = JsonConvert.SerializeObject(cartId);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");


            // Post the content to the API
            var response = await _httpClient.PostAsync(apiUrl, content);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                return true; ; // Success
            }

            return false; // Return null if the request was not successful
        }



        public async Task<bool> removetocart(int cartId)
        {
            var apiUrl = "/api/DeleteToCart"; // The API endpoint for updating the cart

            // Convert cartId to JSON and wrap it in StringContent
            var jsonData = JsonConvert.SerializeObject(cartId);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");


            // Post the content to the API
            var response = await _httpClient.PostAsync(apiUrl, content);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                return true; ; // Success
            }

            return false; // Return null if the request was not successful
        }



    }
}

;