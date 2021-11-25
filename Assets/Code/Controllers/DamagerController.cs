using System;
using System.Collections.Generic;
using Code.Interfaces.Controllers;
using Code.Interfaces.Views;
using Code.Managers;
using Code.Models;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    internal sealed class DamagerController: Controller
    {
        private readonly Dictionary<int, DamagerModel> _damagers;

        public DamagerController()
        {
            _damagers = new Dictionary<int, DamagerModel>();
        }
        
        public override void Setup(SceneViews sceneViews)
        {
            var damagerViews = sceneViews.DamagerViews;
            foreach (var damagerView in damagerViews)
            {
                var damagerModel = new DamagerModel()
                {
                    GameObject = damagerView.gameObject,
                    Transform = damagerView.transform,
                    DamagerView = damagerView,
                    
                    TriggerType = damagerView.TriggerType,
                    DamageRate = damagerView.DamageRate,
                    Damage = damagerView.Damage,
                };
                _damagers.Add(damagerModel.GameObject.GetInstanceID(), damagerModel);
            }
        }
        public override void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Setup(sceneViews);
            Initialization();
        }
        
        public override void Initialization()
        {
            foreach (var damager in _damagers)
            {
                switch (damager.Value.TriggerType)
                {
                    case TriggerManager.Stay:
                        damager.Value.DamagerView.OnStay += OnTrapStay;
                        break;
                    case TriggerManager.Enter:
                        damager.Value.DamagerView.OnEnter += OnTrapEnter;
                        break;
                    case TriggerManager.Exit:
                        damager.Value.DamagerView.OnExit += OnTrapExit;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(damager.Value.TriggerType), $"{damager.Value.TriggerType} не предусмотрен в этом коде");
                };
            }
        }
        
        public override void Cleanup()
        {
            foreach (var damager in _damagers)
            {
                var value = damager.Value;
                
                if (value == null || value.GameObject == null)
                    continue;
                    
                switch (value.TriggerType)
                {
                    case TriggerManager.Stay:
                        value.DamagerView.OnStay -= OnTrapStay;
                        break;
                    case TriggerManager.Enter:
                        value.DamagerView.OnEnter -= OnTrapEnter;
                        break;
                    case TriggerManager.Exit:
                        value.DamagerView.OnExit -= OnTrapExit;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(damager.Value.TriggerType), $"{damager.Value.TriggerType} не предусмотрен в этом коде");
                };
                
                Object.Destroy(value.GameObject);
            }
            _damagers.Clear();
        }
        
        public override void Execute(float deltaTime)
        {
            if (_damagers != null && _damagers.Count != 0)
            {
                foreach (var damager in _damagers)
                {
                    damager.Value.Cooldown -= deltaTime;
                }
            }
        }

        private void OnTrapStay(GameObject gameObject, int damagerID)
        {
            var damager = _damagers[damagerID];
            if (TryUnitDamage(gameObject, damager))
                damager.Cooldown = damager.DamageRate;
        }
        private void OnTrapExit(GameObject gameObject, int damagerID)
        {
            OnTrapEnter(gameObject, damagerID);
        }
        private void OnTrapEnter(GameObject gameObject, int damagerID)
        {
            var damager = _damagers[damagerID];
            TryUnitDamage(gameObject, damager);
        }

        private bool TryUnitDamage(GameObject gameObject, DamagerModel damager)
        {
            if (damager.Cooldown > 0f)
                return false;
            if (gameObject.TryGetComponent(out IUnitView unitView))
            {
                unitView.AddDamage(damager.GameObject, damager.Damage);
                return true;
            }
            return false;
        }
    }
}