using Application.Entities;

namespace Application;

public interface ICarService
{
    public Task<Guid> Add(
        string name,
        string brand,
        int horsepower,
        double engineCapacity,
        CancellationToken cancellationToken);
    public Task<bool> Remove(CancellationToken cancellationToken);
    public Task<IEnumerable<CarEntity>> GetAll(CancellationToken cancellationToken);
}