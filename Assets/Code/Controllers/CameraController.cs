using Code.Configs;
using Code.Controllers.Initializations;
using Code.Interfaces.Controllers;
using Code.Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    internal sealed class CameraController: IController, IUpdate, IStart, ICleanup
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

        public void ReSetup(PlayerInitialization playerInitialization, PlayerConfig config)
        {
            Cleanup();
            Start();
        }
        
        public void Start()
        {
            _player = _playerInitialization.GetPlayer();
            _camera = _playerInitialization.GetCamera();
        }

        public void Update(float deltaTime)
        {
            var playerPosition = _player.Transform.position;
            var cameraPosition = _camera.Transform.position;
            var offset = _config.CameraOffset;
            
            _camera.Transform.position = Vector3.Lerp(cameraPosition, new Vector3(playerPosition.x + offset.x, playerPosition.y + offset.y, cameraPosition.z), _config.CameraSpeed);
        }

        public void Cleanup()
        {
            if (_camera.GameObject != null)
                Object.Destroy(_camera.GameObject);
        }
    }
}