using System;
using System.Collections.Generic;
using Code.Controllers.Initializations;
using Code.Interfaces.Controllers;
using Code.Managers;
using Code.Models;
using Code.Views;
using Pathfinding;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    internal sealed class EnemyController: IController, IStart, IUpdate, ICleanup
    {
        private readonly PlayerInitialization _playerInitialization;
        private readonly Dictionary<int, EnemyModel> _enemyModels;
        private PlayerModel _playerModel;

        private const float DISTANCE_THRESHOLD = 0.2f;

        public EnemyController(PlayerInitialization playerInitialization)
        {
            _playerInitialization = playerInitialization;
            _enemyModels = new Dictionary<int, EnemyModel>();
        }
        
        public void Setup(SceneViews sceneViews)
        {
            var enemyViews = sceneViews.EnemyViews;
            foreach (var enemyView in enemyViews)
            {
                var enemyModel = new EnemyModel()
                {
                    GameObject = enemyView.gameObject,
                    Transform = enemyView.transform,
                    EnemyView = enemyView,
                    
                    EnemyType = enemyView.EnemyType,
                    Waypoints = enemyView.Waypoints,
                    WaypointIndex = 0,
                    AStarAI = enemyView.GetComponent<IAstarAI>(),
                    
                    DamageRate = enemyView.DamageRate,
                    Damage = enemyView.Damage,
                };
                _enemyModels.Add(enemyModel.GameObject.GetInstanceID(), enemyModel);
            }
        }
        public void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Setup(sceneViews);
            Start();
        }
        
        public void Start()
        {
            _playerModel = _playerInitialization.GetPlayer();
            
            foreach (var enemyModel in _enemyModels)
            {
                enemyModel.Value.EnemyView.OnStay += OnPlayerStay;
            }
        }
        
        public void Cleanup()
        {
            foreach (var enemyModel in _enemyModels)
            {
                var value = enemyModel.Value;

                if (value != null && value.GameObject != null)
                {
                    value.EnemyView.OnStay -= OnPlayerStay;
                    Object.Destroy(value.GameObject);
                }
            }
            _enemyModels.Clear();
        }
        
        public void Update(float deltaTime)
        {
            if (_enemyModels != null && _enemyModels.Count != 0)
            {
                foreach (var enemyModel in _enemyModels)
                {
                    var value = enemyModel.Value;
                    value.Cooldown -= deltaTime;

                    switch (value.EnemyType)
                    {
                        case EnemyManager.DefaultEnemy:
                            value.AStarAI.destination = _playerModel.Transform.position;
                            break;
                        case EnemyManager.WayPointsEnemy:
                            if (Mathf.Abs(Vector2.Distance(value.Transform.position, value.AStarAI.destination)) <= DISTANCE_THRESHOLD)
                            {
                                if (value.WaypointIndex >= value.Waypoints.Length - 1)
                                    value.WaypointIndex = 0;
                                else
                                    value.WaypointIndex++;
                            }

                            value.AStarAI.destination = value.Waypoints[value.WaypointIndex].position;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(value.EnemyType), $"{value.EnemyType} не предусмотрен в этом коде");
                    }
                }
            }
        }

        private void OnPlayerStay(GameObject gameObject, int enemyID)
        {
            var enemyModel = _enemyModels[enemyID];
            if (TryPlayerDamage(gameObject, enemyModel))
                enemyModel.Cooldown = enemyModel.DamageRate;
        }

        private bool TryPlayerDamage(GameObject gameObject, EnemyModel enemyModel)
        {
            if (enemyModel.Cooldown > 0f)
                return false;
            
            if (gameObject.TryGetComponent(out PlayerView playerView))
            {
                playerView.AddDamage(enemyModel.GameObject, enemyModel.Damage);
                return true;
            }
            return false;
        }
    }
}