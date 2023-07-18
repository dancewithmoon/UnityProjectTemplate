using Scripts.Infrastructure.Services;

namespace Scripts.Services.Randomizer
{
    public interface IRandomService : IService
    {
        int Next(int minValue, int maxValue);
    }
}