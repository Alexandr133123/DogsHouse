using AutoMapper;
using DogsHouse.Application.Dogs.Queries;
using DogsHouseService.Domain;

namespace DogsHouse.Presentation.Tests.Common
{
    public static class MapperFactory
    {        
        public static IMapper Create()
        {
            var configurationBuilder = new MapperConfiguration(conf =>
            {
                conf.CreateMap<Dog, DogViewModel>();
            });

            return configurationBuilder.CreateMapper();
        }
    }
}
