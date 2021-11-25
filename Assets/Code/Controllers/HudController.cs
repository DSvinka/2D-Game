using Code.Controllers.Initializations;
using Code.Interfaces.Controllers;
using Code.Models;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class HudController: Controller
    {
        private readonly PlayerInitialization _playerInitialization;
        private readonly HudInitialization _hudInitialization;
        
        private PlayerModel _playerModel;
        private HudView _hudView;
        
        public HudController(PlayerInitialization playerInitialization, HudInitialization hudInitialization)
        {
            _playerInitialization = playerInitialization;
            _hudInitialization = hudInitialization;
        }
        
        public override void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Initialization();
        }
        
        public override void Initialization()
        {
            _playerModel = _playerInitialization.GetPlayer();
            _hudView = _hudInitialization.GetHud();
        }
        
        public override void Cleanup()
        {
            if (_hudView != null && _hudView.gameObject != null)
            {
                Object.Destroy(_hudView.gameObject);
            }
        }

        public override void Execute(float deltaTime)
        {
            _hudView.Health.text = $"HP: {_playerModel.Health}";
            _hudView.Coins.text = $"Coins: {_playerModel.Coins}";
        }
    }
}