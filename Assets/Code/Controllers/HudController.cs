using Code.Controllers.Initializations;
using Code.Interfaces.Controllers;
using Code.Models;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class HudController: IController, IStart, IUpdate
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

        public void ReSetup(SceneViews sceneViews)
        {
            Start();
        }
        
        public void Start()
        {
            _playerModel = _playerInitialization.GetPlayer();
            _hudView = _hudInitialization.GetHud();

        }

        public void Update(float deltaTime)
        {
            _hudView.Health.text = $"HP: {_playerModel.Health}";
            _hudView.Coins.text = $"Coins: {_playerModel.Coins}";
        }
    }
}