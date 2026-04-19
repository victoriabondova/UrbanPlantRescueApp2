using System.ComponentModel.DataAnnotations;
using static UrbanPlantRescueApp.Data.Common.DataValidation;

namespace UrbanPlantRescueApp.Services.ViewModels
{
    public class UserProfileFormViewModel
    {
        [MaxLength(UserProfile.FirstNameMaxLength, ErrorMessage = "Името не може да е повече от 50 символа.")]
        [Display(Name = "Име")]
        public string? FirstName { get; set; }
        [MaxLength(UserProfile.LastNameMaxLength, ErrorMessage = "Фамилията не може да е повече от 50 символа.")]
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }
        [MaxLength(UserProfile.BioMaxLength, ErrorMessage = "Биографията не може да е повече от 500 символа.")]
        [Display(Name = "За мен")]
        public string? Bio { get; set; }
    }
}