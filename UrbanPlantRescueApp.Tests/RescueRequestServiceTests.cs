using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrbanPlantRescueApp.Data;
using UrbanPlantRescueApp.Data.Models;
using UrbanPlantRescueApp.Services;

namespace UrbanPlantRescueApp.Tests
{
    public class RescueRequestServiceTests
    {
        private ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }
        private IdentityUser CreateTestUser(string id, string email)
        {
            return new IdentityUser
            {
                Id = id,
                UserName = email,
                Email = email
            };
        }
        [Fact]
        public async Task CreateRescueRequestAsync_CreatesRequestSuccessfully()
        {
            var context = CreateInMemoryContext();
            var user = CreateTestUser("user1", "test@test.com");
            context.Users.Add(user);
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            context.Plants.Add(new Plant
            {
                Id = 1,
                Name = "Тестово растение",
                Description = "Тестово описание",
                CategoryId = 1,
                AddedByUserId = "user1"
            });
            await context.SaveChangesAsync();
            var service = new RescueRequestService(context);
            await service.CreateRescueRequestAsync(1, "user1");
            Assert.Equal(1, context.RescueRequests.Count());
            Assert.Equal("Pending", context.RescueRequests.First().IsApproved);
        }
        [Fact]
        public async Task GetRequestsByPlantIdAsync_ReturnsCorrectRequests()
        {
            var context = CreateInMemoryContext();
            var user = CreateTestUser("user1", "test@test.com");
            context.Users.Add(user);
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            context.Plants.Add(new Plant
            {
                Id = 1,
                Name = "Тестово растение",
                Description = "Тестово описание",
                CategoryId = 1,
                AddedByUserId = "user1"
            });
            context.RescueRequests.Add(new RescueRequest
            {
                Id = 1,
                PlantId = 1,
                RequesterId = "user1",
                RequestedOn = DateTime.UtcNow,
                IsApproved = "Pending"
            });
            await context.SaveChangesAsync();
            var service = new RescueRequestService(context);
            var result = await service.GetRequestsByPlantIdAsync(1);
            Assert.Single(result);
        }
        [Fact]
        public async Task GetAllRequestsAsync_ReturnsAllRequests()
        {
            var context = CreateInMemoryContext();
            var user1 = CreateTestUser("user1", "test1@test.com");
            var user2 = CreateTestUser("user2", "test2@test.com");
            context.Users.AddRange(user1, user2);
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            context.Plants.Add(new Plant
            {
                Id = 1,
                Name = "Тестово растение",
                Description = "Тестово описание",
                CategoryId = 1,
                AddedByUserId = "user1"
            });
            context.RescueRequests.AddRange(
                new RescueRequest
                {
                    Id = 1,
                    PlantId = 1,
                    RequesterId = "user1",
                    RequestedOn = DateTime.UtcNow,
                    IsApproved = "Pending"
                },
                new RescueRequest
                {
                    Id = 2,
                    PlantId = 1,
                    RequesterId = "user2",
                    RequestedOn = DateTime.UtcNow,
                    IsApproved = "Pending"
                }
            );
            await context.SaveChangesAsync();
            var service = new RescueRequestService(context);
            var result = await service.GetAllRequestsAsync();
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task ApproveRequestAsync_ApprovesRequestSuccessfully()
        {
            var context = CreateInMemoryContext();
            var user = CreateTestUser("user1", "test@test.com");
            context.Users.Add(user);
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            context.Plants.Add(new Plant
            {
                Id = 1,
                Name = "Тестово растение",
                Description = "Тестово описание",
                CategoryId = 1,
                AddedByUserId = "user1"
            });
            context.RescueRequests.Add(new RescueRequest
            {
                Id = 1,
                PlantId = 1,
                RequesterId = "user1",
                RequestedOn = DateTime.UtcNow,
                IsApproved = "Pending"
            });
            await context.SaveChangesAsync();
            var service = new RescueRequestService(context);
            await service.ApproveRequestAsync(1);
            Assert.Equal("Approved", context.RescueRequests.First().IsApproved);
        }
    }
}