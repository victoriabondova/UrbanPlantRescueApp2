using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using UrbanPlantRescueApp.Services.Interfaces;

namespace UrbanPlantRescueApp.Web.Controllers
{
    [Authorize]
    public class RescueRequestController : Controller
    {
        private readonly IRescueRequestService rescueRequestService;
        public RescueRequestController(IRescueRequestService rescueRequestService)
        {
            this.rescueRequestService = rescueRequestService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int plantId)
        {
            var requests = await rescueRequestService.GetRequestsByPlantIdAsync(plantId);
            return View(requests);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int plantId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await rescueRequestService.CreateRescueRequestAsync(plantId, userId);
            return RedirectToAction("Details", "Plant", new { id = plantId });
        }
    }
}