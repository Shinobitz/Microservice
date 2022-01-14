using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    // this class is static. Don't have to create an instantce.
    // this class is to generate migration.
    // this class is kind of testing purpose.
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using ( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            // if platforms context doesn't contain anything...
            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data");
                // AddRnage() -> add many data.
                context.Platforms.AddRange(
                    new Platform() {Name="Dotnet", Publisher="Microsoft",Cost="Free"},
                    new Platform() {Name="Kubenetes", Publisher="Microsoft",Cost="Free"},
                    new Platform() {Name="Docker", Publisher="Docker",Cost="Free"}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}