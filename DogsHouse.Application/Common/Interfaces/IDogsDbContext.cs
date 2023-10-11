using DogsHouseService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Application.Common.Interfaces
{
    public interface IDogsDbContext
    {
        DbSet<Dog> Dogs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
