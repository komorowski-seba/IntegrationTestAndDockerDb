using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application;

public interface IApplicationDbContext
{
    DbSet<CarEntity> Cars { get; init; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}