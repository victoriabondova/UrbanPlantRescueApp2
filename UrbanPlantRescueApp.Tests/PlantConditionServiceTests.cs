using Microsoft.EntityFrameworkCore;
using UrbanPlantRescueApp.Data;
using UrbanPlantRescueApp.Data.Models;
using UrbanPlantRescueApp.Data.Common;
using UrbanPlantRescueApp.Services;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Tests
{
    public class PlantConditionServiceTests
    {
        private ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }
        [Fact]
        public async Task AddConditionAsync_AddsConditionSuccessfully()
        {
            var context = CreateInMemoryContext();
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
            var service = new PlantConditionService(context);
            var model = new PlantConditionFormViewModel
            {
                Notes = "Растението изглежда зле",
                Status = "Critical",
                PlantId = 1
            };
            await service.AddConditionAsync(model);
            Assert.Equal(1, context.PlantConditions.Count());
            Assert.Equal("Растението изглежда зле", context.PlantConditions.First().Notes);
        }
        [Fact]
        public async Task GetConditionsByPlantIdAsync_ReturnsCorrectConditions()
        {
            var context = CreateInMemoryContext();
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
            context.PlantConditions.Add(new PlantCondition
            {
                Id = 1,
                Notes = "Тестови бележки",
                Status = PlantConditionStatus.Critical,
                PlantId = 1,
                ReportedOn = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
            var service = new PlantConditionService(context);
            var result = await service.GetConditionsByPlantIdAsync(1);
            Assert.Single(result);
            Assert.Equal("Тестови бележки", result.First().Notes);
        }
        [Fact]
        public async Task DeleteConditionAsync_DeletesConditionSuccessfully()
        {
            var context = CreateInMemoryContext();
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
            context.PlantConditions.Add(new PlantCondition
            {
                Id = 1,
                Notes = "Тестови бележки",
                Status = PlantConditionStatus.Critical,
                PlantId = 1,
                ReportedOn = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
            var service = new PlantConditionService(context);
            await service.DeleteConditionAsync(1);
            Assert.Equal(0, context.PlantConditions.Count());
        }
        [Fact]
        public async Task AddConditionAsync_WithInvalidStatus_UsesCriticalAsDefault()
        {
            var context = CreateInMemoryContext();
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
            var service = new PlantConditionService(context);
            var model = new PlantConditionFormViewModel
            {
                Notes = "Тестови бележки за растението",
                Status = "InvalidStatus",
                PlantId = 1
            };
            await service.AddConditionAsync(model);
            Assert.Equal(PlantConditionStatus.Critical, context.PlantConditions.First().Status);
        }
    }
}