using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UrbanPlantRescueApp.Data.Common.DataValidation;
using static UrbanPlantRescueApp.Data.Common.DataValidation.Category;

namespace UrbanPlantRescueApp.Data.Models
{
    internal class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;
        public virtual ICollection<Plant> Plants { get; set; } = new HashSet<Plant>();
    }
}
