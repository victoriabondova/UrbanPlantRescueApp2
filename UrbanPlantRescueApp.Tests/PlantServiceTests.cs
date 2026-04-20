using Microsoft.EntityFrameworkCore;
using UrbanPlantRescueApp.Data;
using UrbanPlantRescueApp.Data.Models;
using UrbanPlantRescueApp.Services;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Tests
{
    public class PlantServiceTests
    {
        private ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }
        [Fact]
        public async Task GetAllPlantsAsync_ReturnsAllPlants()
        {
            var context = CreateInMemoryContext();
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            context.Plants.Add(new Plant
            {
                Id = 1,
                Name = "Тестово растение",
                Description = "Тестово описание на растението",
                CategoryId = 1,
                AddedByUserId = "user1"
            });
            await context.SaveChangesAsync();
            var service = new PlantService(context);
            var result = await service.GetAllPlantsAsync();
            Assert.Single(result);
            Assert.Equal("Тестово растение", result.First().Name);
        }
        [Fact]
        public async Task GetPlantByIdAsync_ReturnsCorrectPlant()
        {
            var context = CreateInMemoryContext();
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            context.Plants.Add(new Plant
            {
                Id = 1,
                Name = "Тестово растение",
                Description = "Тестово описание на растението",
                CategoryId = 1,
                AddedByUserId = "user1"
            });
            await context.SaveChangesAsync();
            var service = new PlantService(context);
            var result = await service.GetPlantByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Тестово растение", result.Name);
        }
        [Fact]
        public async Task GetPlantByIdAsync_ReturnsNull_WhenNotFound()
        {
            var context = CreateInMemoryContext();
            var service = new PlantService(context);
            var result = await service.GetPlantByIdAsync(999);
            Assert.Null(result);
        }
        [Fact]
        public async Task AddPlantAsync_AddsPlantSuccessfully()
        {
            var context = CreateInMemoryContext();
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            var service = new PlantService(context);
            var model = new PlantFormViewModel
            {
                Name = "Ново растение",
                Description = "Описание на новото растение",
                CategoryId = 1,
                ImageUrl = "https://test.com/image.jpg"
            };
            await service.AddPlantAsync(model, "user1");
            Assert.Equal(1, context.Plants.Count());
            Assert.Equal("Ново растение", context.Plants.First().Name);
        }
        [Fact]
        public async Task DeletePlantAsync_DeletesPlantSuccessfully()
        {
            var context = CreateInMemoryContext();
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            context.Plants.Add(new Plant
            {
                Id = 1,
                Name = "Тестово растение",
                Description = "Тестово описание на растението",
                CategoryId = 1,
                AddedByUserId = "user1"
            });
            await context.SaveChangesAsync();
            var service = new PlantService(context);
            await service.DeletePlantAsync(1);
            Assert.Equal(0, context.Plants.Count());
        }
        [Fact]
        public async Task IsOwnerAsync_ReturnsTrue_WhenUserIsOwner()
        {
            var context = CreateInMemoryContext();
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            context.Plants.Add(new Plant
            {
                Id = 1,
                Name = "Тестово растение",
                Description = "Тестово описание на растението",
                CategoryId = 1,
                AddedByUserId = "user1"
            });
            await context.SaveChangesAsync();
            var service = new PlantService(context);
            var result = await service.IsOwnerAsync(1, "user1");
            Assert.True(result);
        }
        [Fact]
        public async Task IsOwnerAsync_ReturnsFalse_WhenUserIsNotOwner()
        {
            var context = CreateInMemoryContext();
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            context.Plants.Add(new Plant
            {
                Id = 1,
                Name = "Тестово растение",
                Description = "Тестово описание на растението",
                CategoryId = 1,
                AddedByUserId = "user1"
            });
            await context.SaveChangesAsync();
            var service = new PlantService(context);
            var result = await service.IsOwnerAsync(1, "user2");
            Assert.False(result);
        }
        [Fact]
        public async Task EditPlantAsync_EditsPlantSuccessfully()
        {
            var context = CreateInMemoryContext();
            var category = new Category { Id = 1, Name = "Тестова категория" };
            context.Categories.Add(category);
            context.Plants.Add(new Plant
            {
                Id = 1,
                Name = "Старо име",
                Description = "Старо описание на растението",
                CategoryId = 1,
                AddedByUserId = "user1"
            });
            await context.SaveChangesAsync();
            var service = new PlantService(context);
            var model = new PlantFormViewModel
            {
                Name = "Ново име",
                Description = "Ново описание на растението",
                CategoryId = 1
            };
            await service.EditPlantAsync(1, model);
            var plant = context.Plants.First();
            Assert.Equal("Ново име", plant.Name);
            Assert.Equal("Ново описание на растението", plant.Description);
        }
    }
}
