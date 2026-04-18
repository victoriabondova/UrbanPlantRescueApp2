using System.ComponentModel.DataAnnotations;
using static UrbanPlantRescueApp.Data.Common.DataValidation.Comment;

namespace UrbanPlantRescueApp.Services.ViewModels
{
    public class CommentFormViewModel
    {
        [Required(ErrorMessage = "Коментарът е задължителен.")]
        [MinLength(ContentMinLength, ErrorMessage = "Коментарът трябва да е поне 2 символа.")]
        [MaxLength(ContentMaxLength, ErrorMessage = "Коментарът не може да е повече от 500 символа.")]
        public string Content { get; set; } = null!;
        public int PlantId { get; set; }
    }
}