using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> GetCommentsByPlantIdAsync(int plantId);
        Task AddCommentAsync(CommentFormViewModel model, string authorId);
        Task DeleteCommentAsync(int commentId);
        Task<bool> IsAuthorAsync(int commentId, string userId);
    }
}