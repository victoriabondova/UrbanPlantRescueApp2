using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrbanPlantRescueApp.Data;
using UrbanPlantRescueApp.Data.Models;
using UrbanPlantRescueApp.Services;
using UrbanPlantRescueApp.Services.ViewModels;

namespace UrbanPlantRescueApp.Tests
{
    public class CommentServiceTests
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
        public async Task AddCommentAsync_AddsCommentSuccessfully()
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
            var service = new CommentService(context);
            var model = new CommentFormViewModel
            {
                Content = "Тестов коментар",
                PlantId = 1
            };
            await service.AddCommentAsync(model, "user1");
            Assert.Equal(1, context.Comments.Count());
            Assert.Equal("Тестов коментар", context.Comments.First().Content);
        }
        [Fact]
        public async Task GetCommentsByPlantIdAsync_ReturnsCorrectComments()
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
            context.Comments.Add(new Comment
            {
                Id = 1,
                Content = "Тестов коментар",
                PlantId = 1,
                AuthorId = "user1",
                CreatedOn = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
            var service = new CommentService(context);
            var result = await service.GetCommentsByPlantIdAsync(1);
            Assert.Single(result);
            Assert.Equal("Тестов коментар", result.First().Content);
        }
        [Fact]
        public async Task DeleteCommentAsync_DeletesCommentSuccessfully()
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
            context.Comments.Add(new Comment
            {
                Id = 1,
                Content = "Тестов коментар",
                PlantId = 1,
                AuthorId = "user1",
                CreatedOn = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
            var service = new CommentService(context);
            await service.DeleteCommentAsync(1);
            Assert.Equal(0, context.Comments.Count());
        }
        [Fact]
        public async Task IsAuthorAsync_ReturnsTrue_WhenUserIsAuthor()
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
            context.Comments.Add(new Comment
            {
                Id = 1,
                Content = "Тестов коментар",
                PlantId = 1,
                AuthorId = "user1",
                CreatedOn = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
            var service = new CommentService(context);
            var result = await service.IsAuthorAsync(1, "user1");
            Assert.True(result);
        }
        [Fact]
        public async Task IsAuthorAsync_ReturnsFalse_WhenUserIsNotAuthor()
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
            context.Comments.Add(new Comment
            {
                Id = 1,
                Content = "Тестов коментар",
                PlantId = 1,
                AuthorId = "user1",
                CreatedOn = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
            var service = new CommentService(context);
            var result = await service.IsAuthorAsync(1, "user2");
            Assert.False(result);
        }
    }
}