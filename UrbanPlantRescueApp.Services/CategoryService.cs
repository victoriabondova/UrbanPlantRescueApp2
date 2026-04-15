using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanPlantRescueApp.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using UrbanPlantRescueApp.Data;
using UrbanPlantRescueApp.Data.Models;
using UrbanPlantRescueApp.Services.Interfaces;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            return await dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();
        }
        public async Task AddCategoryAsync(CategoryFormViewModel model)
        {
            var category = new Category { Name = model.Name };
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }
        public async Task<bool> CategoryExistsAsync(string name)
        {
            return await dbContext.Categories.AnyAsync(c => c.Name == name);
        }
    }
}