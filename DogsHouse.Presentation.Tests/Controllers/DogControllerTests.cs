using AutoMapper;
using DogsHouse.Api.Controllers;
using DogsHouse.Application.Dogs.Commands.CreateDog;
using DogsHouse.Application.Dogs.Commands.DeleteDog;
using DogsHouse.Application.Dogs.Commands.UpdateDog;
using DogsHouse.Application.Dogs.Queries;
using DogsHouse.Application.Dogs.Queries.GetDogs;
using DogsHouse.Presentation.Tests.Common;
using DogsHouseService.Domain;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DogsHouse.Presentation.Tests.Controllers
{
    public class DogControllerTests
    {
        private readonly IMapper _mapper;

        public DogControllerTests() 
        {
            _mapper = MapperFactory.Create();
        }

        [Fact]
        public async Task DogController_Should_Return_Dogs()
        {
            var testDogs = GetTestData();

            Mock<IMediator> mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetDogsQuery>(), CancellationToken.None))
                .ReturnsAsync(testDogs);
            var testMappedDogs = _mapper.Map<List<DogViewModel>>(testDogs);

            var controller = new DogController(mockMediator.Object, _mapper);

            var result = await controller.GetDogs() as OkObjectResult;
            var resultDogs = result?.Value as List<DogViewModel>;

            result.Should().NotBeNull();
            resultDogs.Should().BeEquivalentTo(testMappedDogs);
        }
        [Fact]
        public async Task DogController_Should_Return_New_DogId()
        {
            var dogViewModel = new DogViewModel
            {
                Name = "test",
                Color = "test color",
                TailLength = 10,
                Weight = 30
            };

            int testNewDogId = 13;

            Mock<IMediator> mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CreateDogCommand>(), CancellationToken.None))
                .ReturnsAsync(testNewDogId);

            var controller = new DogController(mockMediator.Object, _mapper);

            var result = await controller.CreateDog(dogViewModel) as OkObjectResult;
            var resultId = (int?)result?.Value;

            result.Should().NotBeNull();  
            resultId.Should().Be(testNewDogId);
        }

        [Fact]
        public async Task DogController_Should_Return_Ok_On_Update_Dog()
        {
            var dogViewModel = new DogViewModel
            {
                Name = "test",
                Color = "test color",
                TailLength = 10,
                Weight = 30
            };
            int succeessStatusCode = 200;

            Mock<IMediator> mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateDogCommand>(), CancellationToken.None));
            var controller = new DogController(mockMediator.Object, _mapper);

            var result = await controller.UpdateDog(dogViewModel) as OkResult;
            var resultStatusCode = result?.StatusCode;

            resultStatusCode.Should().NotBeNull();
            resultStatusCode.Should().Be(succeessStatusCode);
        }

        [Fact]
        public async Task DogController_Should_Return_Ok_On_Delete_Dog()
        {
            int id = 2;
            int succeessStatusCode = 200;

            Mock<IMediator> mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteDogCommand>(), CancellationToken.None));
            var controller = new DogController(mockMediator.Object, _mapper);

            var result = await controller.DeleteDog(id) as OkResult;
            var resultStatusCode = result?.StatusCode;

            resultStatusCode.Should().NotBeNull();
            resultStatusCode.Should().Be(succeessStatusCode);
        }

        private List<Dog> GetTestData()
        {
            return new List<Dog>
            {
                 new Dog
                 {
                    Name = "Rex",
                    Color = "Brown",
                    TailLength = 20,
                    Weight = 30,
                 },
                 new Dog
                 {
                    Name = "Fido",
                    Color = "Black",
                    TailLength = 15,
                    Weight = 25,
                 },
                 new Dog
                 {
                    Name = "Bella",
                    Color = "White",
                    TailLength = 25,
                    Weight = 22,
                 }
            };
        }
    }
}
