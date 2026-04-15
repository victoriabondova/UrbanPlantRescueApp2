namespace UrbanPlantRescueApp.Services.ViewModels
{
    public class PlantViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; } = null!;
        public string AddedByUserId { get; set; } = null!;
    }
}