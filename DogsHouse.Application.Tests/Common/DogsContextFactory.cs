using DogsHouse.DataAccess.Data;
using DogsHouseService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Application.Tests.Common
{
    public class DogsContextFactory
    {
        public DogsDbContext Create()
        {
            var options = new DbContextOptionsBuilder<DogsDbContext>()
                .UseInMemoryDatabase($"TestDb + {DateTime.Now.Ticks}")
                .Options;

            var context = new DogsDbContext(options);
            context.Database.EnsureCreated();

            context.Dogs.AddRange(
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

            context.SaveChanges();
            return context;
        }
    }
}
