using DogsHouse.Application.Common.Exceptions;
using DogsHouse.Application.Common.Interfaces;
using DogsHouseService.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            var dogsQuery = _dbContext.Dogs.AsQueryable();

            if (!String.IsNullOrEmpty(query.Attribute) && !String.IsNullOrEmpty(query.Order))
            {                
                ApplySorting(ref dogsQuery, query.Attribute, query.Order);
            }

            if (query.PageNumber >= 0 && query.PageSize > 0)
            {
                ApplyPagination(ref dogsQuery, query.PageSize, query.PageNumber);
            }            

            return await dogsQuery.ToListAsync(cancellationToken);
        }

        private void ApplyPagination(ref IQueryable<Dog> query, int pageSize, int pageNumber)
        {
                query = query
                    .Skip(pageSize * pageNumber)
                    .Take(pageSize);
        }

        private void ApplySorting(ref IQueryable<Dog> query, string attribute, string order)
        {           
            var sortingAttribute = GetSortAttribute(attribute);

            if (order == SortOrder.Descending)
            {
                query = query.OrderByDescending(sortingAttribute);
            }
            else if (order == SortOrder.Ascending)
            {
                query = query.OrderBy(sortingAttribute);
            }
        }

        private Expression<Func<Dog, object>> GetSortAttribute(string attribute)
        {
            var attributeAccessor = new Dictionary<string, Expression<Func<Dog, object>>>
            {
                { "name", dog => dog.Name },
                { "color", dog => dog.Color },
                { "weight", dog => dog.Weight },
                { "tailLength", dog => dog.TailLength }
            };

            if (!attributeAccessor.TryGetValue(attribute, out var selectedAttribute))
            {
                throw new SortArgumentNotFoundException(attribute);
            }

            return selectedAttribute;
        }

        private static class SortOrder
        {
            public const string Descending = "desc";
            public const string Ascending = "asc";
        }
    }
}
