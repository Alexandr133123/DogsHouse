using DogsHouse.DataAccess.Data;

namespace DogsHouse.Application.Tests.Common
{
    public abstract class TestBase
    {
        private DogsContextFactory _contextFactory = new DogsContextFactory();
        public DogsDbContext CreateDbContext()
        {
            return _contextFactory.Create();
        }
    }
}
