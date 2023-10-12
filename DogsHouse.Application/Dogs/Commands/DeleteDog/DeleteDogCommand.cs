using MediatR;

namespace DogsHouse.Application.Dogs.Commands.DeleteDog
{
    public class DeleteDogCommand : IRequest
    {
        public int Id { get; set; }
    }
}
