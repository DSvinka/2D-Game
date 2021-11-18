using System;
using System.Collections.Generic;
using Code.Configs;
using Code.Interfaces.Controllers;
using Code.Interfaces.Views;
using Code.Models;
using Code.Utils.Modules;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class BulletController : IEmitter
    {
        private readonly Dictionary<int, PoolModel> _bulletModels;

        private readonly BulletConfig _bulletConfig;
        private readonly PoolService _poolService;

        public BulletController(BulletConfig bulletConfig, PoolService poolService)
        {
            _bulletConfig = bulletConfig;
            _poolService = poolService;

            _bulletModels = new Dictionary<int, PoolModel>();
        }

        private void OnBulletEnter(GameObject gameObject, int bulletID)
        {
            var bullet = _bulletModels[bulletID];
            if (gameObject.TryGetComponent(out IUnitView unitView))
                unitView.AddDamage(gameObject, _bulletConfig.Damage);
            
            if (!bullet.GameObject.TryGetComponent(out BulletView bulletView))
                throw new Exception("BulletView не найден в префабе пули");
            
            // Не самый лучший вариант.
            var trailRenderers = bullet.GameObject.GetComponentsInChildren<TrailRenderer>();
            if (trailRenderers.Length != 0)
            {
                foreach (var trailRenderer in trailRenderers)
                {
                    trailRenderer.Clear();
                }
            }

            bulletView.OnEnter -= OnBulletEnter;
            _bulletModels.Remove(bulletID);
            _poolService.Destroy(bullet);
        }
        
        public void Trow(Vector3 position, Vector3 velocity)
        {
            var poolModel = _poolService.Instantiate(_bulletConfig.Prefab);
            if (!poolModel.GameObject.TryGetComponent(out BulletView bulletView))
                throw new Exception("BulletView не найден в префабе пули");

            bulletView.OnEnter += OnBulletEnter;
            
            SetVelocity(poolModel.Transform, velocity);
            
            poolModel.Transform.position = position;
            poolModel.Rigidbody.velocity = Vector2.zero;
            poolModel.Rigidbody.AddForce(velocity, ForceMode2D.Impulse);
            
            _bulletModels.Add(poolModel.GameObject.GetInstanceID(), poolModel);
        }

        private void SetVelocity(Transform transform, Vector3 velocity)
        {
            var angle = Vector3.Angle(Vector3.left, velocity);
            var axis = Vector3.Cross(Vector3.left, velocity);
            
            transform.rotation = Quaternion.AngleAxis(angle, axis);
        }

    }
}