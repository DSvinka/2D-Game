using System.Collections.Generic;
using Code.Interfaces.Controllers;
using Code.Models;
using UnityEngine;

namespace Code.Utils.Modules
{
    internal sealed class PoolService: IController, ICleanup
    {
        private readonly Dictionary<string, ObjectPool> _cache;

        public PoolService()
        { 
            _cache = new Dictionary<string, ObjectPool>(32);
        }
        
        public PoolModel Instantiate(GameObject prefab)
        {
            if (_cache.TryGetValue(prefab.name, out var viewPool)) 
                return viewPool.Pop();
            
            viewPool = new ObjectPool(prefab);
            _cache[prefab.name] = viewPool;
            return viewPool.Pop();
        }

        public void Destroy(PoolModel poolModel)
        {
            _cache[poolModel.GameObject.name].Push(poolModel); 
        }

        public void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
        }

        public void Cleanup()
        {
            foreach (var objectPool in _cache)
            {
                objectPool.Value.Cleanup();
            }
            _cache.Clear();
        }
    }
}