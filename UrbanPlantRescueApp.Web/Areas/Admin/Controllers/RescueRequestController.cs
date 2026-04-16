using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanPlantRescueApp.Services.Interfaces;

namespace UrbanPlantRescueApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RescueRequestController : Controller
    {
        private readonly IRescueRequestService rescueRequestService;
        public RescueRequestController(IRescueRequestService rescueRequestService)
        {
            this.rescueRequestService = rescueRequestService;
        }
        public async Task<IActionResult> Index()
        {
            var requests = await rescueRequestService.GetAllRequestsAsync();
            return View(requests);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int requestId)
        {
            await rescueRequestService.ApproveRequestAsync(requestId);
            return RedirectToAction(nameof(Index));
        }
    }
}