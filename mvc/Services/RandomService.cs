using mvc.Interfaces;

namespace mvc.Services;
public class RandomService : IRandomService
{
    private readonly int _value;

    public RandomService()
    {
        _value = new Random().Next(1, 1000);
    }

    public int GetRandomNumber() => _value;
}