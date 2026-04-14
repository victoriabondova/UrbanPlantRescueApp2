namespace UrbanPlantRescueApp.Services.ViewModels
{
    internal class RescueRequestViewModel
    {
        public int Id { get; set; }
        public string PlantName { get; set; } = null!;
        public string RequesterEmail { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = null!;
    }
}