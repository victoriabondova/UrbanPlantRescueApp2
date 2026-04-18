using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanPlantRescueApp.Data.Models;

namespace UrbanPlantRescueApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Plant> Plants { get; set; } = null!;
        public DbSet<RescueRequest> RescueRequests { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<PlantCondition> PlantsConditions { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}