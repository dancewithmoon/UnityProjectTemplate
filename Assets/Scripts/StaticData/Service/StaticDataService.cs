using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Infrastructure.AssetManagement;
using Scripts.UI.Screens;
using Scripts.UI.Services.Screens;

namespace Scripts.StaticData.Service
{
    public class StaticDataService : IStaticDataService
    {
        private const string ScreensPath = "Screens";

        private readonly IAssets _assets;
        
        private Dictionary<ScreenId, BaseScreen> _screens;

        public StaticDataService(IAssets assets)
        {
            _assets = assets;
        }

        public async Task Load()
        {
            await Task.WhenAll(
                LoadScreens());
        }

        public BaseScreen GetScreen(ScreenId screenId) =>
            _screens.TryGetValue(screenId, out BaseScreen screen)
                ? screen
                : null;

        private async Task LoadScreens()
        {
            ScreenStaticData screensData = await _assets.Load<ScreenStaticData>(ScreensPath);
            _screens = new Dictionary<ScreenId, BaseScreen>(screensData.Screens);
        }
    }
}