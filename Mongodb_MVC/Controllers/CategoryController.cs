
using Microsoft.AspNetCore.Mvc;
using Mongodb_MVC.Models;
using Mongodb_MVC.Services;

namespace Mongodb_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MongoDBService _mongoDBService;

        public CategoryController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        public async Task<IActionResult> Index() => View(await _mongoDBService.GetCategoriesAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _mongoDBService.AddCategoryAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }

}
