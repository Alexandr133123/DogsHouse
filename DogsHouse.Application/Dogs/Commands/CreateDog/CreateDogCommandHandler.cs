using DogsHouse.Application.Common.Interfaces;
using DogsHouseService.Domain;
using MediatR;
using Serilog;

namespace DogsHouse.Application.Dogs.Commands.CreateDog
{
    public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, int>
    {
        private readonly IDogsDbContext _dbContext;
        public CreateDogCommandHandler(IDogsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateDogCommand command, CancellationToken cancellationToken)
        {
            var dog = new Dog
            {
                Name = command.Name,
                Color = command.Color,
                TailLength = command.TailLength,
                Weight = command.Weight,
            };

            _dbContext.Dogs.Add(dog);
            await _dbContext.SaveChangesAsync(cancellationToken);

            Log.Information($"Dog {dog.Name} was created");

            return dog.Id;
        }
    }
}
