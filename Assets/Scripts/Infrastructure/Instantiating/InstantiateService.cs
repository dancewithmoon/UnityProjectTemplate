using UnityEngine;

namespace Scripts.Infrastructure.Instantiating
{
    public class InstantiateService : IInstantiateService
    {
        public GameObject Instantiate(GameObject prefab) => 
            Object.Instantiate(prefab);

        public GameObject Instantiate(GameObject prefab, Vector3 at) => 
            Object.Instantiate(prefab, at, Quaternion.identity);

        public GameObject Instantiate(GameObject prefab, Vector3 at, Transform parent) => 
            Object.Instantiate(prefab, at, Quaternion.identity, parent);

        public GameObject Instantiate(GameObject prefab, Transform parent) => 
            Object.Instantiate(prefab, parent);
    }
}