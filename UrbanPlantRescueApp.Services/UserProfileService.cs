using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrbanPlantRescueApp.Data;
using UrbanPlantRescueApp.Data.Models;
using UrbanPlantRescueApp.Services.Interfaces;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        public UserProfileService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<UserProfileViewModel?> GetProfileAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return null;
            var profile = await dbContext.UserProfiles
                .FirstOrDefaultAsync(up => up.UserId == userId);
            var plantsCount = await dbContext.Plants
                .CountAsync(p => p.AddedByUserId == userId);
            var requestsCount = await dbContext.RescueRequests
                .CountAsync(r => r.RequesterId == userId);
            return new UserProfileViewModel
            {
                Email = user.Email!,
                FirstName = profile?.FirstName,
                LastName = profile?.LastName,
                Bio = profile?.Bio,
                RegisteredOn = profile?.RegisteredOn ?? DateTime.UtcNow,
                PlantsCount = plantsCount,
                RescueRequestsCount = requestsCount
            };
        }
        public async Task<UserProfileFormViewModel> GetProfileForEditAsync(string userId)
        {
            var profile = await dbContext.UserProfiles
                .FirstOrDefaultAsync(up => up.UserId == userId);
            return new UserProfileFormViewModel
            {
                FirstName = profile?.FirstName,
                LastName = profile?.LastName,
                Bio = profile?.Bio
            };
        }
        public async Task SaveProfileAsync(string userId, UserProfileFormViewModel model)
        {
            var profile = await dbContext.UserProfiles
                .FirstOrDefaultAsync(up => up.UserId == userId);
            if (profile == null)
            {
                profile = new UserProfile
                {
                    UserId = userId,
                    RegisteredOn = DateTime.UtcNow
                };
                await dbContext.UserProfiles.AddAsync(profile);
            }
            profile.FirstName = model.FirstName;
            profile.LastName = model.LastName;
            profile.Bio = model.Bio;
            await dbContext.SaveChangesAsync();
        }
    }
}