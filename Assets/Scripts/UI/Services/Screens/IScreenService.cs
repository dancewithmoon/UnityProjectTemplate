using Scripts.Infrastructure.Services;

namespace Scripts.UI.Services.Screens
{
    public interface IScreenService : IService
    {
        void Open(ScreenId screenId);
    }
}