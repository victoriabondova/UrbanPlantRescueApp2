namespace UrbanPlantRescueApp.Services.ViewModels
{
    public class PlantListViewModel
    {
        public IEnumerable<PlantViewModel> Plants { get; set; } = new List<PlantViewModel>();
        public string? SearchTerm { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 6;
    }
}