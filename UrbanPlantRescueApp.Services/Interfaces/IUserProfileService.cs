using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileViewModel?> GetProfileAsync(string userId);
        Task<UserProfileFormViewModel> GetProfileForEditAsync(string userId);
        Task SaveProfileAsync(string userId, UserProfileFormViewModel model);
    }
}