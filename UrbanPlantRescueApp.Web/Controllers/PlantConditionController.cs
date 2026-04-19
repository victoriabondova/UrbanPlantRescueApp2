using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanPlantRescueApp.Services.Interfaces;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Web.Controllers
{
    [Authorize]
    public class PlantConditionController : Controller
    {
        private readonly IPlantConditionService plantConditionService;
        public PlantConditionController(IPlantConditionService plantConditionService)
        {
            this.plantConditionService = plantConditionService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlantConditionFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Plant", new { id = model.PlantId });
            }
            await plantConditionService.AddConditionAsync(model);
            return RedirectToAction("Details", "Plant", new { id = model.PlantId });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int conditionId, int plantId)
        {
            await plantConditionService.DeleteConditionAsync(conditionId);
            return RedirectToAction("Details", "Plant", new { id = plantId });
        }
    }
}