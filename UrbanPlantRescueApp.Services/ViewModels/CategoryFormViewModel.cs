using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static UrbanPlantRescueApp.Data.Common.DataValidation.Category;
using UrbanPlantRescueApp.Data.Common;

namespace UrbanPlantRescueApp.Services.ViewModels
{
    public class CategoryFormViewModel
    {
        [Required(ErrorMessage = "Името е задължително.")]
        [MinLength(DataValidation.Category.CategoryNameMinLength, ErrorMessage = "Името трябва да е поне 2 символа.")]
        [MaxLength(DataValidation.Category.CategoryNameMaxLength, ErrorMessage = "Името не може да е повече от 100 символа.")]
        public string Name { get; set; } = null!;
    }
}