using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UrbanPlantRescueApp.Data.Common.DataValidation;

namespace UrbanPlantRescueApp.Services.ViewModels
{
    internal class PlantFormViewModel
    {
        [Required(ErrorMessage = "Името е задължително.")]
        [MinLength(Plant.NameMinLength, ErrorMessage = "Името трябва да е поне 2 символа.")]
        [MaxLength(Plant.NameMaxLength, ErrorMessage = "Името не може да е повече от 150 символа.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Описанието е задължително.")]
        [MinLength(Plant.DescriptionMinLength, ErrorMessage = "Описанието трябва да е поне 10 символа.")]
        [MaxLength(Plant.DescriptionMaxLength, ErrorMessage = "Описанието не може да е повече от 1500 символа.")]
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Моля изберете категория.")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}