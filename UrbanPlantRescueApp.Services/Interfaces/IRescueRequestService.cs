using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Services.Interfaces
{
    public interface IRescueRequestService
    {
        Task CreateRescueRequestAsync(int plantId, string requesterId);
        Task<IEnumerable<RescueRequestViewModel>> GetRequestsByPlantIdAsync(int plantId);
        Task<IEnumerable<RescueRequestViewModel>> GetAllRequestsAsync();
        Task ApproveRequestAsync(int requestId);
    }
}