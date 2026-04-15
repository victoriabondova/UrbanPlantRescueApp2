using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanPlantRescueApp.Data.Common;
using UrbanPlantRescueApp.Data.Models;
using static UrbanPlantRescueApp.Data.Common.DataValidation.Plant;

namespace UrbanPlantRescueApp.Data.Configuration
{
    public class PlantEntityConfiguration : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> entity)
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(DataValidation.Plant.NameMaxLength);
            entity.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(DataValidation.Plant.DescriptionMaxLength);
            entity.HasOne(p => p.Category)
                .WithMany(c => c.Plants)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(p => p.AddedByUser)
                .WithMany()
                .HasForeignKey(p => p.AddedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}