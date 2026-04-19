using System.ComponentModel.DataAnnotations;
using static UrbanPlantRescueApp.Data.Common.DataValidation.PlantCondition;

namespace UrbanPlantRescueApp.Services.ViewModels
{
    public class PlantConditionFormViewModel
    {
        [Required(ErrorMessage = "Бележките са задължителни.")]
        [MinLength(NotesMinLength, ErrorMessage = "Бележките трябва да са поне 2 символа.")]
        [MaxLength(NotesMaxLength, ErrorMessage = "Бележките не могат да са повече от 1000 символа.")]
        public string Notes { get; set; } = null!;
        [Required(ErrorMessage = "Статусът е задължителен.")]
        public string Status { get; set; } = "Critical";
        public int PlantId { get; set; }
    }
}