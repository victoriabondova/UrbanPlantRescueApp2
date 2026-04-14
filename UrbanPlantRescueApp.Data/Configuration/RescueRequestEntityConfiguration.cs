using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanPlantRescueApp.Data.Models;

namespace UrbanPlantRescueApp.Data.Configuration
{
    internal class RescueRequestEntityConfiguration : IEntityTypeConfiguration<RescueRequest>
    {
        public void Configure(EntityTypeBuilder<RescueRequest> entity)
        {
            entity.HasKey(r => r.Id);
            entity.HasOne(r => r.Plant)
                .WithMany(p => p.RescueRequests)
                .HasForeignKey(r => r.PlantId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(r => r.Requester)
                .WithMany()
                .HasForeignKey(r => r.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
