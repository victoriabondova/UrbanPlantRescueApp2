namespace UrbanPlantRescueApp.Services.ViewModels
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public DateTime RegisteredOn { get; set; }
        public int PlantsCount { get; set; }
        public int RescueRequestsCount { get; set; }
    }
}