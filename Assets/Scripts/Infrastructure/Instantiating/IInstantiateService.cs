using UnityEngine;

namespace Scripts.Infrastructure.Instantiating
{
    public interface IInstantiateService
    {
        GameObject Instantiate(GameObject prefab);
        GameObject Instantiate(GameObject prefab, Vector3 at);
        GameObject Instantiate(GameObject prefab, Vector3 at, Transform parent);
        GameObject Instantiate(GameObject prefab, Transform parent);
    }
}