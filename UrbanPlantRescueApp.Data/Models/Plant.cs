using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UrbanPlantRescueApp.Data.Common.DataValidation.Plant;

namespace UrbanPlantRescueApp.Data.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        [Required]
        public string AddedByUserId { get; set; } = null!;
        public virtual IdentityUser AddedByUser { get; set; } = null!;
        public virtual ICollection<RescueRequest> RescueRequests { get; set; } = new List<RescueRequest>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<PlantCondition> Conditions { get; set; } = new List<PlantCondition>();
    }
}