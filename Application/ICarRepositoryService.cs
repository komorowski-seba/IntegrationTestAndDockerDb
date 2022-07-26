using Application.Entities;

namespace Application;

public interface ICarRepositoryService
{
    public Task Add(CarEntity carEntity, CancellationToken cancellationToken);
    public Task Remove(Guid carId, CancellationToken cancellationToken);
    public Task<IEnumerable<CarEntity>> GetCars(CancellationToken cancellationToken);
}