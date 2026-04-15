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
    public class RescueRequestService : IRescueRequestService
    {
        private readonly ApplicationDbContext dbContext;
        public RescueRequestService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateRescueRequestAsync(int plantId, string requesterId)
        {
            var request = new RescueRequest
            {
                PlantId = plantId,
                RequesterId = requesterId,
                RequestedOn = DateTime.Now,
                IsApproved = "Pending"
            };
            await dbContext.RescueRequests.AddAsync(request);
            await dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<RescueRequestViewModel>> GetRequestsByPlantIdAsync(int plantId)
        {
            return await dbContext.RescueRequests
                .Where(r => r.PlantId == plantId)
                .Include(r => r.Requester)
                .Include(r => r.Plant)
                .Select(r => new RescueRequestViewModel
                {
                    Id = r.Id,
                    PlantName = r.Plant.Name,
                    RequesterEmail = r.Requester.Email!,
                    RequestDate = r.RequestedOn,
                    Status = r.IsApproved
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<RescueRequestViewModel>> GetAllRequestsAsync()
        {
            return await dbContext.RescueRequests
                .Include(r => r.Requester)
                .Include(r => r.Plant)
                .Select(r => new RescueRequestViewModel
                {
                    Id = r.Id,
                    PlantName = r.Plant.Name,
                    RequesterEmail = r.Requester.Email!,
                    RequestDate = r.RequestedOn,
                    Status = r.IsApproved
                })
                .ToListAsync();
        }
        public async Task ApproveRequestAsync(int requestId)
        {
            var request = await dbContext.RescueRequests.FindAsync(requestId);
            if (request != null)
            {
                request.IsApproved = "Approved";
                await dbContext.SaveChangesAsync();
            }
        }
    }
}