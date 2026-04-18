using Microsoft.EntityFrameworkCore;
using UrbanPlantRescueApp.Data;
using UrbanPlantRescueApp.Data.Models;
using UrbanPlantRescueApp.Services.Interfaces;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext dbContext;
        public CommentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<CommentViewModel>> GetCommentsByPlantIdAsync(int plantId)
        {
            return await dbContext.Comments
                .Where(c => c.PlantId == plantId)
                .Include(c => c.Author)
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    AuthorEmail = c.Author.Email!,
                    AuthorId = c.AuthorId,
                    CreatedOn = c.CreatedOn
                })
                .ToListAsync();
        }
        public async Task AddCommentAsync(CommentFormViewModel model, string authorId)
        {
            var comment = new Comment
            {
                Content = model.Content,
                PlantId = model.PlantId,
                AuthorId = authorId,
                CreatedOn = DateTime.UtcNow
            };
            await dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await dbContext.Comments.FindAsync(commentId);
            if (comment != null)
            {
                dbContext.Comments.Remove(comment);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> IsAuthorAsync(int commentId, string userId)
        {
            var comment = await dbContext.Comments.FindAsync(commentId);
            if (comment == null) return false;
            return comment.AuthorId == userId;
        }
    }
}