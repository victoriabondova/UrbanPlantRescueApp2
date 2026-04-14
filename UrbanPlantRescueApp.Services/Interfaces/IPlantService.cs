using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services.Interfaces
{
    internal interface IPlantService
    {
        Task<IEnumerable<PlantViewModel>> GetAllPlantsAsync();
        Task<PlantViewModel?> GetPlantByIdAsync(int id);
        Task AddPlantAsync(PlantFormViewModel model, string userId);
        Task<PlantFormViewModel?> GetPlantForEditAsync(int id);
        Task EditPlantAsync(int id, PlantFormViewModel model);
        Task DeletePlantAsync(int id);
        Task<bool> IsOwnerAsync(int plantId, string userId);
    }
}
