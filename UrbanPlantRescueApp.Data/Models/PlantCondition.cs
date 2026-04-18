using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanPlantRescueApp.Data.Common;
using static UrbanPlantRescueApp.Data.Common.DataValidation.PlantCondition;

namespace UrbanPlantRescueApp.Data.Models
{
    public  class PlantCondition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(NotesMaxLength)]
        public string Notes { get; set; } = null!;
        [Required]
        public PlantConditionStatus Status { get; set; } = PlantConditionStatus.Critical;
        [Required]
        public DateTime ReportedOn { get; set; } = DateTime.UtcNow;
        [Required]
        public int PlantId { get; set; }
        [ForeignKey(nameof(PlantId))]
        public virtual Plant Plant { get; set; } = null!;
    }
}