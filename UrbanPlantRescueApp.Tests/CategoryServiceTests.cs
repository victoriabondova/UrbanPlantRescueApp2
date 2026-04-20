using Microsoft.EntityFrameworkCore;
using UrbanPlantRescueApp.Data;
using UrbanPlantRescueApp.Data.Models;
using UrbanPlantRescueApp.Services;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Tests
{
    public class CategoryServiceTests
    {
        private ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }
        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsAllCategories()
        {
            var context = CreateInMemoryContext();
            context.Categories.AddRange(
                new Category { Id = 1, Name = "Стайни растения" },
                new Category { Id = 2, Name = "Сукуленти" }
            );
            await context.SaveChangesAsync();
            var service = new CategoryService(context);
            var result = await service.GetAllCategoriesAsync();
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task AddCategoryAsync_AddsCategorySuccessfully()
        {
            var context = CreateInMemoryContext();
            var service = new CategoryService(context);
            var model = new CategoryFormViewModel { Name = "Нова категория" };
            await service.AddCategoryAsync(model);
            Assert.Equal(1, context.Categories.Count());
            Assert.Equal("Нова категория", context.Categories.First().Name);
        }
        [Fact]
        public async Task CategoryExistsAsync_ReturnsTrue_WhenCategoryExists()
        {
            var context = CreateInMemoryContext();
            context.Categories.Add(new Category { Id = 1, Name = "Стайни растения" });
            await context.SaveChangesAsync();
            var service = new CategoryService(context);
            var result = await service.CategoryExistsAsync("Стайни растения");
            Assert.True(result);
        }
        [Fact]
        public async Task CategoryExistsAsync_ReturnsFalse_WhenCategoryDoesNotExist()
        {
            var context = CreateInMemoryContext();
            var service = new CategoryService(context);
            var result = await service.CategoryExistsAsync("Несъществуваща категория");
            Assert.False(result);
        }
        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsEmpty_WhenNoCategories()
        {
            var context = CreateInMemoryContext();
            var service = new CategoryService(context);
            var result = await service.GetAllCategoriesAsync();
            Assert.Empty(result);
        }
    }
}