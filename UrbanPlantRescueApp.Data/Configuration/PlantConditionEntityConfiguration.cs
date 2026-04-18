using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanPlantRescueApp.Data.Models;
using static UrbanPlantRescueApp.Data.Common.DataValidation.PlantCondition;

namespace UrbanPlantRescueApp.Data.Configuration
{
    public class PlantConditionEntityConfiguration : IEntityTypeConfiguration<PlantCondition>
    {
        public void Configure(EntityTypeBuilder<PlantCondition> entity)
        {
            entity.HasKey(pc => pc.Id);
            entity.Property(pc => pc.Notes)
                .IsRequired()
                .HasMaxLength(NotesMaxLength);
            entity.Property(pc => pc.Status)
                .IsRequired()
                .HasConversion<string>();
            entity.HasOne(pc => pc.Plant)
                .WithMany(p => p.Conditions)
                .HasForeignKey(pc => pc.PlantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
