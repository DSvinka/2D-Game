using System;
using Code.Configs;
using Code.Controllers.Initializations;
using Code.Interfaces.Controllers;
using Code.Managers;
using Code.Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    internal sealed class PlayerController: IController, IStart, ICleanup
    {
        private PlayerInitialization _playerInitialization;
        private PlayerConfig _config;
        private PlayerModel _player;

        public PlayerController(PlayerInitialization playerInitialization, PlayerConfig playerConfig)
        {
            _playerInitialization = playerInitialization;
            _config = playerConfig;
        }
        
        public void ReSetup()
        {
            Cleanup();
            Start();
        }
        
        public void Start()
        {
            _player = _playerInitialization.GetPlayer();

            var view = _player.View;
            view.OnHealing += AddHealth;
            view.OnDamage += AddDamage;
            view.OnPickup += Pickup;
        }

        public void Cleanup()
        {
            var view = _player.View;
            if (view != null)
            {
                view.OnHealing -= AddHealth;
                view.OnDamage -= AddDamage;
                view.OnPickup -= Pickup;
                Object.Destroy(view);
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
                    throw new ArgumentOutOfRangeException($"{pickupType} не поддерживается в этом коде.");
            }
        }

        private void Death()
        {
            // TODO: Сделать нормальную смерть
            _player.GameObject.SetActive(false);
        }
    }
}