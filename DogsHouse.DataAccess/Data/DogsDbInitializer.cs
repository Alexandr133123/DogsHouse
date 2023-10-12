using DogsHouseService.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DogsHouse.DataAccess.Data
{
    public static class DogsDbInitializer
    {
        public static async Task InitializeDbAsync(this WebApplication app)
        {
            var dbContext = app.Services
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<DogsDbContext>();            
            
            dbContext.Database.EnsureCreated();

            if (!dbContext.Dogs.Any())
            {
                await Seed(dbContext);
            }
        }

        private static async Task Seed(DogsDbContext dbContext)
        {
            dbContext.Dogs.AddRange(
                    new Dog
                    {
                        Name = "Rex",
                        Color = "Brown",
                        TailLength = 20,
                        Weight = 30,
                    },
                    new Dog
                    {
                        Name = "Fido",
                        Color = "Black",
                        TailLength = 15,
                        Weight = 25,
                    },
                    new Dog
                    {
                        Name = "Bella",
                        Color = "White",
                        TailLength = 25,
                        Weight = 22,
                    },
                    new Dog
                    {
                        Name = "Rocky",
                        Color = "Gray",
                        TailLength = 18,
                        Weight = 35,
                    },
                    new Dog
                    {
                        Name = "Luna",
                        Color = "Golden",
                        TailLength = 22,
                        Weight = 28,
                    },
                    new Dog
                    {
                        Name = "Charlie",
                        Color = "Spotted",
                        TailLength = 17,
                        Weight = 31,
                    },
                    new Dog
                    {
                        Name = "Daisy",
                        Color = "Cream",
                        TailLength = 19,
                        Weight = 29,
                    },
                    new Dog
                    {
                        Name = "Buddy",
                        Color = "Red",
                        TailLength = 21,
                        Weight = 27,
                    },
                    new Dog
                    {
                        Name = "Milo",
                        Color = "Brindle",
                        TailLength = 16,
                        Weight = 33,
                    },
                    new Dog
                    {
                        Name = "Max",
                        Color = "Sable",
                        TailLength = 23,
                        Weight = 26,
                    }
                );

            await dbContext.SaveChangesAsync();
        }
    }
}
