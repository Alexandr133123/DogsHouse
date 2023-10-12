using DogsHouse.Application.Common.Exceptions;
using DogsHouse.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DogsHouse.Application.Dogs.Commands.DeleteDog
{
    public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand>
    {
        private readonly IDogsDbContext _dbContext;

        public DeleteDogCommandHandler(IDogsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteDogCommand command, CancellationToken cancellationToken)
        {
            var dog = await _dbContext.Dogs
                .FirstOrDefaultAsync(d => d.Id == command.Id);

            if (dog == null)
            {
                throw new NotFoundException(nameof(dog), command.Id);
            }

            _dbContext.Dogs.Remove(dog);
            await _dbContext.SaveChangesAsync(cancellationToken);

            Log.Information($"Dog {dog.Name} was removed");
        }
    }
}
