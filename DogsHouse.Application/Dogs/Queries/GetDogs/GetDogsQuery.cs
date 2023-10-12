using DogsHouseService.Domain;
using MediatR;

namespace DogsHouse.Application.Dogs.Queries.GetDogs
{
    public class GetDogsQuery : IRequest<List<Dog>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Attribute { get; set; } = String.Empty;
        public string Order { get; set; } = String.Empty;
    }
}
