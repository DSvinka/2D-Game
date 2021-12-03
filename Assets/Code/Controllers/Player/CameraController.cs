using Code.Configs;
using Code.Controllers.Initializations;
using Code.Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Player
{
    internal sealed class CameraController: Controller
    {
        private PlayerInitialization _playerInitialization;
        private PlayerConfig _config;
        
        private PlayerModel _player;
        private CameraModel _camera;

        public CameraController(PlayerInitialization playerInitialization, PlayerConfig config)
        {
            _playerInitialization = playerInitialization;
            _config = config;
        }
        
        public override void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Initialization();
        }
        
        public override void Initialization()
        {
            _player = _playerInitialization.GetPlayer();
            _camera = _playerInitialization.GetCamera();
        }

        public override void Execute(float deltaTime)
        {
            var playerPosition = _player.Transform.position;
            var cameraPosition = _camera.Transform.position;
            var offset = _config.CameraOffset;
            
            _camera.Transform.position = Vector3.Lerp(cameraPosition, new Vector3(playerPosition.x + offset.x, playerPosition.y + offset.y, cameraPosition.z), _config.CameraSpeed);
        }

        public override void Cleanup()
        {
            if (_camera != null && _camera.GameObject != null)
                Object.Destroy(_camera.GameObject);
        }
    }
}