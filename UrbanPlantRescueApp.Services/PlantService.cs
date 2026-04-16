using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrbanPlantRescueApp.Data;
using UrbanPlantRescueApp.Data.Models;
using UrbanPlantRescueApp.Services.Interfaces;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services
{
    public class PlantService : IPlantService
    {
        private readonly ApplicationDbContext dbContext;
        public PlantService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<PlantViewModel>> GetAllPlantsAsync()
        {
            return await dbContext.Plants
                .Include(p => p.Category)
                .Select(p => new PlantViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    AddedByUserId = p.AddedByUserId
                })
                .ToListAsync();
        }
        public async Task<PlantViewModel?> GetPlantByIdAsync(int id)
        {
            return await dbContext.Plants
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .Select(p => new PlantViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    AddedByUserId = p.AddedByUserId
                })
                .FirstOrDefaultAsync();
        }
        public async Task AddPlantAsync(PlantFormViewModel model, string userId)
        {
            var plant = new Plant
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                AddedByUserId = userId
            };
            await dbContext.Plants.AddAsync(plant);
            await dbContext.SaveChangesAsync();
        }
        public async Task<PlantFormViewModel?> GetPlantForEditAsync(int id)
        {
            var plant = await dbContext.Plants.FindAsync(id);
            if (plant == null) return null;
            return new PlantFormViewModel
            {
                Name = plant.Name,
                Description = plant.Description,
                CategoryId = plant.CategoryId,
                ImageUrl = plant.ImageUrl
            };
        }
        public async Task EditPlantAsync(int id, PlantFormViewModel model)
        {
            var plant = await dbContext.Plants.FindAsync(id);
            if (plant != null)
            {
                plant.Name = model.Name;
                plant.Description = model.Description;
                plant.CategoryId = model.CategoryId;
                plant.ImageUrl = model.ImageUrl;
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task DeletePlantAsync(int id)
        {
            var relatedRequests = dbContext.RescueRequests.Where(r => r.PlantId == id);
            dbContext.RescueRequests.RemoveRange(relatedRequests);
            var plant = await dbContext.Plants.FindAsync(id);
            if (plant != null)
            {
                dbContext.Plants.Remove(plant);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> IsOwnerAsync(int plantId, string userId)
        {
            var plant = await dbContext.Plants.FindAsync(plantId);
            if (plant == null) return false;
            return plant.AddedByUserId == userId;
        }
        public async Task<PlantListViewModel> GetFilteredPlantsAsync(string? searchTerm, int page, int pageSize)
        {
            var query = dbContext.Plants
                .Include(p => p.Category)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(searchTerm) ||
                    p.Description.ToLower().Contains(searchTerm) ||
                    p.Category.Name.ToLower().Contains(searchTerm));
            }
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var plants = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PlantViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    AddedByUserId = p.AddedByUserId
                })
                .ToListAsync();
            return new PlantListViewModel
            {
                Plants = plants,
                SearchTerm = searchTerm,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize
            };
        }
        public async Task<IEnumerable<PlantViewModel>> GetPlantsByCategoryAsync(int categoryId)
        {
            return await dbContext.Plants
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new PlantViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    AddedByUserId = p.AddedByUserId
                })
                .ToListAsync();
        }
    }
}