using AutoMapper;
using DogsHouseService.Domain;

namespace DogsHouse.Application.Dogs.Queries
{
    public class DogViewModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int TailLength { get; set; }
        public double Weight { get; set; }
    }
}
