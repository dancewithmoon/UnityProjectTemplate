using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Infrastructure.AssetManagement
{
    public class ResourcesAssets : IAssets
    {
        private readonly Dictionary<string, Object> _completedCache =
            new Dictionary<string, Object>();

        private readonly Dictionary<string, IEnumerable<Object>> _completedArraysCache =
            new Dictionary<string, IEnumerable<Object>>();

        public Task<T> Load<T>(object source) where T : Object
        {
            if (source is T sourceObject)
                return Task.FromResult(sourceObject);
            
            if (source is string path)
            {
                if (_completedCache.TryGetValue(path, out Object cachedObject))
                    return Task.FromResult(cachedObject as T);

                T asset = Resources.Load<T>(path);
                _completedCache.Add(path, asset);
                return Task.FromResult(asset);
            }

            throw new Exception("Source Type mismatch!");
        }

        public Task<IEnumerable<T>> LoadAll<T>(string path) where T : Object
        {
            if (_completedArraysCache.TryGetValue(path, out IEnumerable<Object> cachedAssets))
                return Task.FromResult(cachedAssets.Cast<T>());
            
            T[] assets = Resources.LoadAll<T>(path);
            _completedArraysCache.Add(path, assets);
            return Task.FromResult((IEnumerable<T>)assets);
        }

        public void CleanUp()
        {
            foreach (Object asset in _completedCache.Values)
            {
                Resources.UnloadAsset(asset);
            }
            _completedCache.Clear();
        }
    }
}