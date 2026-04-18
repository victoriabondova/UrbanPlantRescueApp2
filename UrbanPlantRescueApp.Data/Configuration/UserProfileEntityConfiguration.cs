using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanPlantRescueApp.Data.Models;
using static UrbanPlantRescueApp.Data.Common.DataValidation.UserProfile;

namespace UrbanPlantRescueApp.Data.Configuration
{
    public class UserProfileEntityConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> entity)
        {
            entity.HasKey(up => up.Id);
            entity.Property(up => up.FirstName)
                .HasMaxLength(FirstNameMaxLength);
            entity.Property(up => up.LastName)
                .HasMaxLength(LastNameMaxLength);
            entity.Property(up => up.Bio)
                .HasMaxLength(BioMaxLength);
            entity.HasOne(up => up.User)
                .WithMany()
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
