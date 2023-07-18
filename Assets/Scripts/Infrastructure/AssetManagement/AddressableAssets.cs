using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;

namespace Scripts.Infrastructure.AssetManagement
{
    public class AddressableAssets : IAssets
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();

        private readonly Dictionary<string, AsyncOperationHandle> _currentlyLoading = new();

        public AddressableAssets() => Addressables.InitializeAsync();

        public async Task<T> Load<T>(object source) where T : Object
        {
            return source switch
            {
                string path => await LoadByPath<T>(path),
                AssetReference reference => await LoadByReference<T>(reference),
                _ => throw new Exception("Source Type mismatch!")
            };
        }

        public async Task<IEnumerable<T>> LoadAll<T>(string path) where T : Object
        {
            IList<IResourceLocation> locations = await Addressables.LoadResourceLocationsAsync(path, typeof(T)).Task;
            List<Task<T>> tasks = locations.Select(location => LoadByPath<T>(location.PrimaryKey)).ToList();
            T[] assets = await Task.WhenAll(tasks);
            return assets;
        }

        public void CleanUp()
        {
            _currentlyLoading.Clear();
            foreach (AsyncOperationHandle asset in _completedCache.Values)
            {
                Addressables.Release(asset);
            }
            _completedCache.Clear();
        }

        private async Task<T> LoadByPath<T>(string path) where T : Object
        {
            return await LoadAsset<T>(path, path);
        }

        private async Task<T> LoadByReference<T>(AssetReference reference) where T : Object
        {
            return await LoadAsset<T>(reference.AssetGUID, reference);
        }

        private async Task<T> LoadAsset<T>(string key, object source) where T : Object
        {
            if (_completedCache.TryGetValue(key, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            if (_currentlyLoading.TryGetValue(key, out AsyncOperationHandle loadingHandle))
                return await loadingHandle.Task as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(source);
            _currentlyLoading.Add(key, handle);
            await handle.Task;
            _currentlyLoading.Remove(key);
            _completedCache.Add(key, handle);
            return handle.Result;
        }
    }
}