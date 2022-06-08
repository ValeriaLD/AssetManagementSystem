using AssetManagementSystem.Server.Models;
using AssetManagementSystem.Shared;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AssetManagementSystem.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    IDNP = "2004027078235",
                    FirstName = "Valeria",
                    LastName = "Leascenco",
                    Email = "valeria.leascenco@iis.utm.md",
                    PersonalPhone = "+37360649004"
                });
        }
    }
}