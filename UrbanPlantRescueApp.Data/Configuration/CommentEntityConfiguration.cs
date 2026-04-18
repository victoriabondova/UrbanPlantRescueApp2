using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanPlantRescueApp.Data.Models;
using static UrbanPlantRescueApp.Data.Common.DataValidation.Comment;

namespace UrbanPlantRescueApp.Data.Configuration
{
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(ContentMaxLength);
            entity.HasOne(c => c.Plant)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PlantId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(c => c.Author)
                .WithMany()
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
