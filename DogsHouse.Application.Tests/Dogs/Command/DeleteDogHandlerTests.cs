using DogsHouse.Application.Common.Exceptions;
using DogsHouse.Application.Dogs.Commands.DeleteDog;
using DogsHouse.Application.Tests.Common;
using DogsHouseService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Application.Tests.Dogs.Command
{
    public class DeleteDogHandlerTests : TestBase
    {
        [Fact]
        public async void DeleteDogHandler_Should_Delete_Dog()
        {
            using var _dbContext = CreateDbContext();

            var command = new DeleteDogCommand()
            {
                Id = 2
            };

            var handler = new DeleteDogCommandHandler(_dbContext);

            await handler.Handle(command, CancellationToken.None);

            var dog = await _dbContext.Dogs.FirstOrDefaultAsync(d => d.Id == command.Id);

            Assert.Null(dog);
        }

        [Fact] 
        public async void DeleteDogHandler_Should_Throw_Exception()
        {
            using var _dbContext = CreateDbContext();

            var command = new DeleteDogCommand()
            {
                Id = 100
            };
            var handler = new DeleteDogCommandHandler(_dbContext);


            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal(ex.Message, $"Entity dog (id: {command.Id}) not found.");
        }
    }
}
