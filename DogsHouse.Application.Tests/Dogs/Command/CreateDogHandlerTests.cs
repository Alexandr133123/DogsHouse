using DogsHouse.Application.Dogs.Commands.CreateDog;
using DogsHouse.Application.Tests.Common;
using DogsHouseService.Domain;

namespace DogsHouse.Application.Tests.DogsTests.CommandTests
{
    public class CreateDogHandlerTests : TestBase
    {
        [Fact]
        public async Task CreateDogCommandHandler_Should_Create_Dog()
        {
            using var _dbContext = CreateDbContext();

            var command = new CreateDogCommand
            {
                Name = "Fido",
                Color = "Brown",
                TailLength = 10,
                Weight = 20
            };
            var handler = new CreateDogCommandHandler(_dbContext);

            var dogId = await handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(_dbContext.Dogs.Count(), dogId);
            Assert.NotNull(_dbContext.Dogs.FirstOrDefault(d => d.Id == dogId));
        }
    }
}
