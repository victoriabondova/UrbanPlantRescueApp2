using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanPlantRescueApp.Data.Common;
using UrbanPlantRescueApp.Data.Models;
using static UrbanPlantRescueApp.Data.Common.DataValidation.Category;

namespace UrbanPlantRescueApp.Data.Configuration
{
    internal class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        private readonly IEnumerable<Category> Categories = new List<Category>
        {
            new Category { Id = 1, Name = "Стайни растения" },
            new Category { Id = 2, Name = "Градински цветя" },
            new Category { Id = 3, Name = "Сукуленти и кактуси" },
            new Category { Id = 4, Name = "Цъфтящи растения" },
            new Category { Id = 5, Name = "Билки и подправки" }
        };

        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(DataValidation.Category.CategoryNameMaxLength);
            entity.HasData(Categories);
        }
    }
}
