using System.Threading.Tasks;
using Scripts.Infrastructure.Services;
using Scripts.UI.Screens;
using Scripts.UI.Services.Screens;

namespace Scripts.StaticData.Service
{
    public interface IStaticDataService : IService
    {
        Task Load();
        BaseScreen GetScreen(ScreenId screenId);
    }
}