
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services.Interfaces
{
    public interface IPlantConditionService
    {
        Task<IEnumerable<PlantConditionViewModel>> GetConditionsByPlantIdAsync(int plantId);
        Task AddConditionAsync(PlantConditionFormViewModel model);
        Task DeleteConditionAsync(int conditionId);
    }
}