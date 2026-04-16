using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrbanPlantRescueApp.Services.Interfaces;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Web.Controllers
{
    [Authorize]
    public class PlantController : Controller
    {
        private readonly IPlantService plantService;
        private readonly ICategoryService categoryService;
        private readonly IRescueRequestService rescueRequestService;
        public PlantController(IPlantService plantService, ICategoryService categoryService, IRescueRequestService rescueRequestService)
        {
            this.plantService = plantService;
            this.categoryService = categoryService;
            this.rescueRequestService = rescueRequestService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchTerm, int page = 1)
        {
            var model = await plantService.GetFilteredPlantsAsync(searchTerm, page, 6);
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var plant = await plantService.GetPlantByIdAsync(id);
            if (plant == null) return NotFound();
            ViewBag.Requests = await rescueRequestService.GetRequestsByPlantIdAsync(id);
            return View(plant);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            var model = new PlantFormViewModel
            {
                Categories = categories
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PlantFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return View(model);
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await plantService.AddPlantAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            bool isAdmin = User.IsInRole("Admin");
            if (!isAdmin && !await plantService.IsOwnerAsync(id, userId))
            {
                return Unauthorized();
            }
            var model = await plantService.GetPlantForEditAsync(id);
            if (model == null) return NotFound();
            model.Categories = await categoryService.GetAllCategoriesAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlantFormViewModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            bool isAdmin = User.IsInRole("Admin");
            if (!isAdmin && !await plantService.IsOwnerAsync(id, userId)) { return Unauthorized(); }
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return View(model);
            }
            await plantService.EditPlantAsync(id, model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            bool isAdmin = User.IsInRole("Admin");
            if (!isAdmin && !await plantService.IsOwnerAsync(id, userId)) { return Unauthorized(); }
            var plant = await plantService.GetPlantByIdAsync(id);
            if (plant == null) return NotFound();
            return View(plant);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            bool isAdmin = User.IsInRole("Admin");
            if (!isAdmin && !await plantService.IsOwnerAsync(id, userId))  { return Unauthorized(); }
            await plantService.DeletePlantAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}