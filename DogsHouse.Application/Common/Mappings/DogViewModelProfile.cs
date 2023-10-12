using AutoMapper;
using DogsHouse.Application.Dogs.Queries;
using DogsHouseService.Domain;

namespace DogsHouse.Application.Common.Mappings
{
    public class DogViewModelProfile : Profile
    {
        public DogViewModelProfile() 
        {
            CreateMap<Dog, DogViewModel>();
        }
    }
}
