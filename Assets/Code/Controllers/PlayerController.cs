using Code.Configs;
using Code.Controllers.Initializations;
using Code.Interfaces.Controllers;
using Code.Models;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class PlayerController: IController, IStart, ICleanup
    {
        private PlayerInitialization _playerInitialization;
        private PlayerConfig _config;
        private PlayerModel _player;

        public PlayerController(PlayerInitialization playerPlayerInitialization, PlayerConfig playerConfig)
        {
            Setup(playerPlayerInitialization, playerConfig);
        }
        
        public void ReSetup(PlayerInitialization playerInitialization, PlayerConfig playerConfig)
        {
            Cleanup();
            Setup(playerInitialization, playerConfig);
            Start();
        }
        
        private void Setup(PlayerInitialization playerInitialization, PlayerConfig playerConfig)
        {
            _playerInitialization = playerInitialization;
            _config = playerConfig;
        }
        
        public void Start()
        {
            _player = _playerInitialization.GetPlayer();

            var view = _player.View;
            view.OnHealing += AddHealth;
            view.OnDamage += AddDamage;
        }

        public void Cleanup()
        {
            var view = _player.View;
            if (view != null)
            {
                view.OnHealing -= AddHealth;
                view.OnDamage -= AddDamage;
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
        
        private void Death()
        {
            // TODO: Сделать нормальную смерть
            _player.GameObject.SetActive(false);
        }
    }
}