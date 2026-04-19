namespace UrbanPlantRescueApp.Services.ViewModels
{
    public class PlantConditionViewModel
    {
        public int Id { get; set; }
        public string Notes { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime ReportedOn { get; set; }
    }
}