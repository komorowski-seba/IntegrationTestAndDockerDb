using Application.Dto;
using Application.Entities;

namespace Application.Extensions;

public static class CarExtension
{
    public static CarDto ToDto(this CarEntity car)
    {
        var result = new CarDto
        {
            Brand = car.Brand,
            EngineCapacity = car.EngineCapacity,
            Horsepower = car.Horsepower,
            Name = car.Name
        };
        return result;
    }
}