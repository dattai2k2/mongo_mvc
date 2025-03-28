using Microsoft.AspNetCore.Mvc;
using Mongodb_MVC.Models;
using Mongodb_MVC.Services;

namespace Mongodb_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly MongoDBService _mongoDBService;

        public ProductController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        public async Task<IActionResult> Index() => View(await _mongoDBService.GetProductsAsync());

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _mongoDBService.GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreateDate = DateTime.Now;
                await _mongoDBService.AddProductAsync(product);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = await _mongoDBService.GetCategoriesAsync();
            return View(product);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }

}
