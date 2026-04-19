using Microsoft.EntityFrameworkCore;
using UrbanPlantRescueApp.Data;
using UrbanPlantRescueApp.Data.Common;
using UrbanPlantRescueApp.Data.Models;
using UrbanPlantRescueApp.Services.Interfaces;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services
{
    public class PlantConditionService : IPlantConditionService
    {
        private readonly ApplicationDbContext dbContext;
        public PlantConditionService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<PlantConditionViewModel>> GetConditionsByPlantIdAsync(int plantId)
        {
            return await dbContext.PlantConditions
                .Where(pc => pc.PlantId == plantId)
                .OrderByDescending(pc => pc.ReportedOn)
                .Select(pc => new PlantConditionViewModel
                {
                    Id = pc.Id,
                    Notes = pc.Notes,
                    Status = pc.Status.ToString(),
                    ReportedOn = pc.ReportedOn
                })
                .ToListAsync();
        }
        public async Task AddConditionAsync(PlantConditionFormViewModel model)
        {
            if (!Enum.TryParse<PlantConditionStatus>(model.Status, out var status))
            {
                status = PlantConditionStatus.Critical;
            }
            var condition = new PlantCondition
            {
                Notes = model.Notes,
                Status = status,
                PlantId = model.PlantId,
                ReportedOn = DateTime.UtcNow
            };
            await dbContext.PlantConditions.AddAsync(condition);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteConditionAsync(int conditionId)
        {
            var condition = await dbContext.PlantConditions.FindAsync(conditionId);
            if (condition != null)
            {
                dbContext.PlantConditions.Remove(condition);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}