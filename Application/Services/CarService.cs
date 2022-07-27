using Application.Entities;

namespace Application.Services;

public class CarService : ICarService
{
    private readonly ICarRepositoryService _carRepository;

    public CarService(ICarRepositoryService carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<Guid> Add(
        string name,
        string brand,
        int horsepower,
        double engineCapacity,
        CancellationToken cancellationToken)
    {
        var newCar = new CarEntity(name, brand, horsepower, engineCapacity);
        await _carRepository.Add(newCar, cancellationToken);
        return newCar.Id;
    }

    public async Task<bool> Remove(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CarEntity>> GetAll(CancellationToken cancellationToken)
    {
        return Enumerable.Empty<CarEntity>();
    }
}