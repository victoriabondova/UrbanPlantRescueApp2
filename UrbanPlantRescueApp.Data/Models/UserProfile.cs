using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UrbanPlantRescueApp.Data.Common.DataValidation.UserProfile;

namespace UrbanPlantRescueApp.Data.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; } = null!;
        [MaxLength(FirstNameMaxLength)]
        public string? FirstName { get; set; }
        [MaxLength(LastNameMaxLength)]
        public string? LastName { get; set; }
        [MaxLength(BioMaxLength)]
        public string? Bio { get; set; }
        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;
    }
}