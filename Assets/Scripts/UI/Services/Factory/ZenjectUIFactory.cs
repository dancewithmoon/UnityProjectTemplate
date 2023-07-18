using System.Threading.Tasks;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Instantiating;
using Scripts.StaticData.Service;
using UnityEngine;

namespace Scripts.UI.Services.Factory
{
    public class ZenjectUIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IInstantiateService _instantiateService;
        private readonly IStaticDataService _staticData;
        private Transform _uiRoot;

        public ZenjectUIFactory(IAssets assets, IInstantiateService instantiateService, IStaticDataService staticData)
        {
            _assets = assets;
            _instantiateService = instantiateService;
            _staticData = staticData;
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetPath.UIRootPath);
        }

        public async void CreateUIRoot()
        {
            GameObject rootPrefab = await _assets.Load<GameObject>(AssetPath.UIRootPath);
            _uiRoot = _instantiateService.Instantiate(rootPrefab).transform;
        }
    }
}