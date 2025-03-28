
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

        public async Task<IActionResult> Index(string text)
        {
            return View(await _mongoDBService.SearchCategoriesAsync(text));
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {

                category.CreateDate = DateTime.Now;
                await _mongoDBService.AddCategoryAsync(category);

                return RedirectToAction("Index");
            }
            return View(category);
        }
        public async Task<IActionResult> Update(string id)
        {
            Category category = await _mongoDBService.GetCategoryByIdAsync(id);

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            await _mongoDBService.UpdateCategoryAsync(category.Id, category);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }

}
