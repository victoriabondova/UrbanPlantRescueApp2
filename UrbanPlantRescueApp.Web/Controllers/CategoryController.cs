using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UrbanPlantRescueApp.Services.Interfaces;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryFormViewModel model)
        {
            if (await categoryService.CategoryExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "Тази категория вече съществува!");
            }
            if (!ModelState.IsValid) { return View(model); }
            await categoryService.AddCategoryAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}