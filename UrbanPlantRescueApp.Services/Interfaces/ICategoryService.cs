using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
        Task AddCategoryAsync(CategoryFormViewModel model);
        Task<bool> CategoryExistsAsync(string name);
    }
}