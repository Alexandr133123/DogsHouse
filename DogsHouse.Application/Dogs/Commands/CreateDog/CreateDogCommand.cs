using MediatR;

namespace DogsHouse.Application.Dogs.Commands.CreateDog
{
    public class CreateDogCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int TailLength { get; set; }
        public double Weight { get; set; }

    }
}
