using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrbanPlantRescueApp.Services.Interfaces;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Plant", new { id = model.PlantId });
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await commentService.AddCommentAsync(model, userId);
            return RedirectToAction("Details", "Plant", new { id = model.PlantId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int commentId, int plantId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            bool isAdmin = User.IsInRole("Admin");
            if (!isAdmin && !await commentService.IsAuthorAsync(commentId, userId))
            {
                return Unauthorized();
            }
            await commentService.DeleteCommentAsync(commentId);
            return RedirectToAction("Details", "Plant", new { id = plantId });
        }
    }
}