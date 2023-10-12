using DogsHouse.Application.Common.Exceptions;
using DogsHouse.Application.Dogs.Commands.UpdateDog;
using DogsHouse.Application.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Application.Tests.Dogs.Command
{
    public class UpdateDogHandlerTests : TestBase
    {
        [Fact]
        public async void UpdateDogHandler_Should_Update_Dog() 
        {
            using var _dbContext = CreateDbContext();

            var command = new UpdateDogCommand
            {
                Id = 2,
                Color = "TestColor",
                Name = "Name",
                TailLength = 1,
                Weight = 1
            };
            var handler = new UpdateDogCommandHandler(_dbContext);

            await handler.Handle(command, CancellationToken.None);

            var updatedDog = await _dbContext.Dogs.FirstOrDefaultAsync(d => d.Id == command.Id);

            Assert.NotNull(updatedDog);
            Assert.Equal(command.Name, updatedDog.Name);
            Assert.Equal(command.Color, updatedDog.Color);
            Assert.Equal(command.TailLength, updatedDog.TailLength);
            Assert.Equal(command.Weight, updatedDog.Weight);
        }

        [Fact]
        public async void UpdateDogHandler_Should_Throw_Exception()
        {
            using var _dbContext = CreateDbContext();

            var command = new UpdateDogCommand
            {
                Id = 100
            };

            var handler = new UpdateDogCommandHandler(_dbContext);

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal(ex.Message, $"Entity dog (id: {command.Id}) not found.");
        }
    }
}
