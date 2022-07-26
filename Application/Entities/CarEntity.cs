namespace Application.Entities;

public class CarEntity : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string Brand { get; private set; }
    public int Horsepower { get; private set; }
    public double EngineCapacity { get; private set; }

    public CarEntity(string name, string brand, int horsepower, double engineCapacity)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Brand = brand ?? throw new ArgumentNullException(nameof(brand));
        Horsepower = horsepower;
        EngineCapacity = engineCapacity;
    }
}