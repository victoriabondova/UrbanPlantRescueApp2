using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UrbanPlantRescueApp.Data.Common.DataValidation.Comment;

namespace UrbanPlantRescueApp.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [Required]
        public string AuthorId { get; set; } = null!;
        [ForeignKey(nameof(AuthorId))]
        public virtual IdentityUser Author { get; set; } = null!;
        [Required]
        public int PlantId { get; set; }
        [ForeignKey(nameof(PlantId))]
        public virtual Plant Plant { get; set; } = null!;
    }
}