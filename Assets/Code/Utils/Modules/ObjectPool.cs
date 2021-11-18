using System.Collections.Generic;
using Code.Interfaces.Controllers;
using Code.Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Utils.Modules
{
    internal sealed class ObjectPool: ICleanup
    {
        private readonly Stack<PoolModel> _stack;
        private readonly GameObject _prefab;
        private readonly Transform _root;

        public ObjectPool(GameObject prefab)
        {
            _prefab = prefab;
            _root = new GameObject($"[{_prefab.name}]").transform;
            _stack = new Stack<PoolModel>();
        }

        public PoolModel Pop()
        {
            PoolModel poolModel;
            if (_stack.Count == 0)
            {
                var gameObject = Object.Instantiate(_prefab);
                gameObject.name = _prefab.name;

                poolModel = new PoolModel()
                {
                    GameObject = gameObject,
                    Transform = gameObject.transform,

                    Collider = gameObject.GetComponent<Collider2D>(),
                    Rigidbody = gameObject.GetComponent<Rigidbody2D>(),
                    SpriteRenderer = gameObject.GetComponent<SpriteRenderer>(),
                };
            }
            else
            {
                poolModel = _stack.Pop();
            }
            
            poolModel.GameObject.SetActive(true);
            poolModel.Transform.SetParent(null);
            return poolModel;
        }

        public void Push(PoolModel poolModel)
        {
            _stack.Push(poolModel);
            poolModel.Transform.SetParent(_root);
            poolModel.GameObject.SetActive(false);
        }
        
        public void Cleanup()
        {
            if (_root.gameObject != null)
                Object.Destroy(_root.gameObject);
            _stack.Clear();
        }
    }
}