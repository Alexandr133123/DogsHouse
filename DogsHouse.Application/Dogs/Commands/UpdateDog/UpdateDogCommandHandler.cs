using DogsHouse.Application.Common.Exceptions;
using DogsHouse.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DogsHouse.Application.Dogs.Commands.UpdateDog
{
    public class UpdateDogCommandHandler : IRequestHandler<UpdateDogCommand>
    {
        private readonly IDogsDbContext _dbContext;

        public UpdateDogCommandHandler(IDogsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateDogCommand command, CancellationToken cancellationToken)
        {
            var dog = await _dbContext.Dogs
                .FirstOrDefaultAsync(d => d.Id == command.Id);

            if (dog == null)
            {
                throw new NotFoundException(nameof(dog), command.Id);
            }

            dog.Name = command.Name;
            dog.TailLength = command.TailLength;
            dog.Color = command.Color;
            dog.Weight = command.Weight;    

            await _dbContext.SaveChangesAsync(cancellationToken);

            Log.Information($"Dog {dog.Name} ({dog.Id}) was updated");
        }
    }
}
