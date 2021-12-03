using System;
using Code.Configs;
using Code.Controllers.Initializations;
using Code.Managers;
using Code.Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Player
{
    internal sealed class PlayerController: Controller
    {
        private PlayerInitialization _playerInitialization;
        private PlayerConfig _config;
        private PlayerModel _player;

        public PlayerController(PlayerInitialization playerInitialization, PlayerConfig playerConfig)
        {
            _playerInitialization = playerInitialization;
            _config = playerConfig;
        }
        
        public override void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Initialization();
        }
        
        public override void Initialization()
        {
            _player = _playerInitialization.GetPlayer();

            var view = _player.View;
            view.OnHealing += AddHealth;
            view.OnDamage += AddDamage;
            view.OnPickup += Pickup;
        }

        public override void Cleanup()
        {
            if (_player != null && _player.GameObject != null)
            {
                if (_player.View != null)
                {
                    _player.View.OnHealing -= AddHealth;
                    _player.View.OnDamage -= AddDamage;
                    _player.View.OnPickup -= Pickup;
                }
                Object.Destroy(_player.GameObject);
            }
        }

        private void AddHealth(GameObject healer, int _, float health)
        {
            _player.Health += health;
            if (_player.Health > _config.MaxHealth)
                _player.Health = _config.MaxHealth;
        }

        private void AddDamage(GameObject attacker, int _, float damage)
        {
            _player.Health -= damage;
            if (_player.Health <= 0)
            {
                _player.Health = 0;
                Death();
            }
        }

        private void Pickup(GameObject item, int _, PickupManager pickupType)
        {
            switch (pickupType)
            {
                case PickupManager.Coin:
                    _player.Coins += 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pickupType), $"{pickupType} не поддерживается в этом коде.");
            }
        }

        private void Death()
        {
            // TODO: Сделать нормальную смерть
            _player.GameObject.SetActive(false);
        }
    }
}