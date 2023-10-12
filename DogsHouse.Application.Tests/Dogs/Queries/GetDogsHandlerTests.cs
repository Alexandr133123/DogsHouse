using DogsHouse.Application.Dogs.Queries.GetDogs;
using DogsHouse.Application.Tests.Common;
using FluentAssertions;

namespace DogsHouse.Application.Tests.Dogs.Queries
{
    public class GetDogsHandlerTests : TestBase
    {
        [Fact]
        public async Task GetDogsHandler_Should_Return_All_Dogs()
        {
            using var _dbContext = CreateDbContext();

            var query = new GetDogsQuery();
            var handler = new GetDogsQueryHandler(_dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(_dbContext.Dogs);
        }

        [Fact]
        public async Task GetDogsHandler_Should_Return_Paginated_Dogs()
        {
            using var _dbContext = CreateDbContext();

            var query = new GetDogsQuery
            {
                PageNumber = 1,
                PageSize = 3
            };
            var handler = new GetDogsQueryHandler(_dbContext);
            var testPaginatedDogs = _dbContext.Dogs
                .Skip(query.PageNumber * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(testPaginatedDogs, result);
        }
    }
}
