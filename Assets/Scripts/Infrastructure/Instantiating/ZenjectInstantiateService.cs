using Scripts.Infrastructure.Services.ContainerService;
using UnityEngine;

namespace Scripts.Infrastructure.Instantiating
{
    public class ZenjectInstantiateService : IInstantiateService
    {
        private readonly ContainerService _container;

        public ZenjectInstantiateService(ContainerService container)
        {
            _container = container;
        }
        
        public GameObject Instantiate(GameObject prefab) => 
            _container.Container.InstantiatePrefab(prefab);

        public GameObject Instantiate(GameObject prefab, Vector3 at) =>
            Instantiate(prefab, at, null);

        public GameObject Instantiate(GameObject prefab, Vector3 at, Transform parent) => 
            _container.Container.InstantiatePrefab(prefab, at, Quaternion.identity, parent);

        public GameObject Instantiate(GameObject prefab, Transform parent) => 
            _container.Container.InstantiatePrefab(prefab, parent);
    }
}