using DogsHouse.Application.Common.Exceptions;
using DogsHouse.Application.Common.Interfaces;
using DogsHouseService.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Application.Dogs.Queries.GetDogs
{
    public class GetDogsQueryHandler : IRequestHandler<GetDogsQuery, List<Dog>>
    {
        private readonly IDogsDbContext _dbContext;

        public GetDogsQueryHandler(IDogsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Dog>> Handle(GetDogsQuery query, CancellationToken cancellationToken)
        {
            IQueryable<Dog> dogsQuery = _dbContext.Dogs;

            if (query.PageSize > 0)
            {
                dogsQuery = dogsQuery
                    .Skip(query.PageSize * query.PageNumber)
                    .Take(query.PageSize);
            }

            if (!String.IsNullOrEmpty(query.Attribute) && !String.IsNullOrEmpty(query.Order))
            {
               ApplySorting(ref dogsQuery, query.Attribute, query.Order);
            }            

            var dogs = await dogsQuery.ToListAsync();

            return dogs;
        }

        private void ApplySorting(ref IQueryable<Dog> dogsQuery, string attribute, string order)
        {
            Dictionary<string, Func<Dog, object>> attributeAccessor = new Dictionary<string, Func<Dog, object>>
            {
                { "name", dog => dog.Name },
                { "color", dog => dog.Color },
                { "weight", dog => dog.Weight },
                { "tailLength", dog => dog.TailLength },
            };

            var selectedAttribute = attributeAccessor[attribute];

            if (selectedAttribute == null) 
            {
                throw new SortArgumentNotFoundException(attribute);
            }

            dogsQuery = (order.ToLower() == "desc" 
                    ? dogsQuery.OrderByDescending(selectedAttribute) 
                    : dogsQuery.OrderBy(selectedAttribute))
                .AsQueryable();
        }
    }
}
