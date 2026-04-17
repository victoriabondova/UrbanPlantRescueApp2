using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanPlantRescueApp.Services;
using UrbanPlantRescueApp.Services.Interfaces;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IPlantService plantService;
        public CategoryController(ICategoryService categoryService, IPlantService plantService)
        {
            this.categoryService = categoryService;
            this.plantService = plantService;
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
        [AllowAnonymous]
        public async Task<IActionResult> Plants(int id)
        {
            var plants = await plantService.GetPlantsByCategoryAsync(id);
            var category = (await categoryService.GetAllCategoriesAsync())
                .FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();

            ViewBag.CategoryName = category.Name;
            ViewBag.CategoryId = id;
            return View(plants);
        }
    }
}