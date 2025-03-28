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

        public async Task<IActionResult> Index(string text)
        {
            return View(await _mongoDBService.SearchProductsAsync(text));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _mongoDBService.GetCategoriesByStatusAsync();
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
        public async Task<IActionResult> Update(string id)
        {
            Product product = await _mongoDBService.GetProductByIdAsync(id);
            ViewBag.Categories = await _mongoDBService.GetCategoriesByStatusAsync();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            await _mongoDBService.UpdateProductAsync(product.Id, product);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }

}
