using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotisissimusSimpleSearch.Data;
using NotisissimusSimpleSearch.Models;
using NotisissimusSimpleSearch.Services;
using System.Diagnostics;
using System.Text.Json;

namespace NotisissimusSimpleSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("/search")]
        public async Task<IActionResult> SearchAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
                return Ok(new List<string>());
            try
            {
                var products = await _productService.GetProductViaFTSAsync(query);
                return Ok(JsonSerializer.Serialize(products));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GenerateRandomData(int count)
        {
            _productService.GenerateRandomData(count);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateProduct(string name, string descriprtion)
        {
            _productService.CreateProduct(new Product { Name = name, Description = descriprtion });
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
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
