namespace UrbanPlantRescueApp.Services.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string AuthorEmail { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}