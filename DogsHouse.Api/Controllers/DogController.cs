using AutoMapper;
using DogsHouse.Application.Dogs.Commands.CreateDog;
using DogsHouse.Application.Dogs.Commands.DeleteDog;
using DogsHouse.Application.Dogs.Commands.UpdateDog;
using DogsHouse.Application.Dogs.Queries;
using DogsHouse.Application.Dogs.Queries.GetDogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DogsHouse.Api.Controllers
{
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public DogController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/dogs")]
        public async Task<IActionResult> GetDogs(string? attribute = "", 
            string? order = "", 
            int? pageNumber = 0, 
            int? pageSize = 0)
        
        {
            var query = new GetDogsQuery
            {
                PageNumber = pageNumber.Value,
                PageSize = pageSize.Value,
                Attribute = attribute,
                Order = order,
            };

            var dogs = _mapper.Map<List<DogViewModel>>(await _mediator.Send(query));

            return Ok(dogs);
        }

        [HttpPost]
        [Route("createDog/")]
        public async Task<IActionResult> CreateDog(DogViewModel dog)
        {
            var query = new CreateDogCommand
            {
                Name = dog.Name,
                Color = dog.Color,
                TailLength = dog.TailLength,
                Weight = dog.Weight,
            };

            int? dogId = await _mediator.Send(query);

            return Ok(dogId);
        }
        [HttpPost]
        [Route("updateDog/")]
        public async Task<IActionResult> UpdateDog(DogViewModel dog)
        {
            var query = new UpdateDogCommand
            {
                Id = dog.Id,
                Name = dog.Name,
                Color = dog.Color,
                TailLength = dog.TailLength,
                Weight = dog.Weight,
            };

            await _mediator.Send(query);

            return Ok();
        }

        [HttpDelete]
        [Route("deleteDog/")]
        public async Task<IActionResult> DeleteDog(int id)
        {
            var query = new DeleteDogCommand
            {
                Id = id
            };

            await _mediator.Send(query);

            return Ok();
        }
    }
}
