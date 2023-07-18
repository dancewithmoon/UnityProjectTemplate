using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Instantiating;
using Scripts.Infrastructure.Services.ContainerService;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.StaticData.Service;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public class ZenjectGameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IInstantiateService _instantiateService;
        private readonly IStaticDataService _staticData;
        private readonly ContainerService _container;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject _hero;
        
        public ZenjectGameFactory(IAssets assets, IInstantiateService instantiateService, IStaticDataService staticData,
            ContainerService container)
        {
            _assets = assets;
            _instantiateService = instantiateService;
            _staticData = staticData;
            _container = container;
        }

        public async Task WarmUp()
        {
            await Task.Delay(10);
        }
        
        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            _assets.CleanUp();
        }

        private async Task<GameObject> InstantiateRegistered(string path)
        {
            GameObject asset = await _assets.Load<GameObject>(path);
            return InstantiateRegistered(asset);
        }

        private async Task<GameObject> InstantiateRegistered(string path, Vector3 at)
        {
            GameObject asset = await _assets.Load<GameObject>(path);
            return InstantiateRegistered(asset, at);
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = _instantiateService.Instantiate(prefab);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject gameObject = _instantiateService.Instantiate(prefab, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }
    }
}