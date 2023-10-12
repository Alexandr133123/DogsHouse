using MediatR;

namespace DogsHouse.Application.Dogs.Commands.UpdateDog
{
    public class UpdateDogCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int TailLength { get; set; }
        public double Weight { get; set; }
    }
}
