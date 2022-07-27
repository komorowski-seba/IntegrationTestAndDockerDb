using Application;
using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class CarRepositoryService : ICarRepositoryService
{
    private readonly IApplicationDbContext _applicationDb;

    public CarRepositoryService(IApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }

    public async Task Add(CarEntity carEntity, CancellationToken cancellationToken)
    {
        await _applicationDb.Cars.AddAsync(carEntity, cancellationToken);
        await _applicationDb.SaveChangesAsync(cancellationToken);
    }

    public async Task Remove(Guid carId, CancellationToken cancellationToken)
    {
        var findCar = await _applicationDb
            .Cars
            .FirstOrDefaultAsync(n => n.Id.Equals(carId), cancellationToken);
        if (findCar is null)
            throw new NullReferenceException($"I haven't found car (id:'{carId}'");

        _applicationDb.Cars.Remove(findCar);
        await _applicationDb.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CarEntity>> GetCars(CancellationToken cancellationToken)
    {
        var result = await _applicationDb
            .Cars
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return result;
    }
}