using System;
using Code.Configs;
using Code.Factory;
using Code.Models;
using Code.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Initializations
{
    internal sealed class PlayerInitialization
    {
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerConfig _config;
        
        private PlayerModel _player;
        private CameraModel _camera;

        public PlayerInitialization(PlayerConfig config, PlayerFactory playerFactory)
        {
            _config = config;
            _playerFactory = playerFactory;
        }

        public void Initialization()
        {
            if (_player != null && _player.GameObject != null)
                Object.Destroy(_player.GameObject);
            
            if (_camera != null && _camera.GameObject != null)
                Object.Destroy(_camera.GameObject);
            
            var view = _playerFactory.CreatePlayer();
            if (!view.TryGetComponent(out Collider2D collider))
                throw new Exception("Компонент Collider2D не найден на объекте PlayerView");
            
            if (!view.TryGetComponent(out Rigidbody2D rigidbody))
                throw new Exception("Компонент Rigidbody2D не найден на объекте PlayerView");
            
            if (!view.TryGetComponent(out SpriteRenderer spriteRenderer))
                throw new Exception("Компонент SpriteRenderer не найден на объекте PlayerView");
            
            var playerModel = new PlayerModel()
            {
                Health = _config.MaxHealth,
                View = view,
                GameObject = view.gameObject,
                Transform = view.transform,
                Collider = collider,
                Rigidbody = rigidbody,
                SpriteRenderer = spriteRenderer,
            };

            _player = playerModel;
            _player.Transform.SetParent(null);
            
            var scale = _player.Transform.localScale;
            playerModel.RightScale = scale;
            playerModel.LeftScale = scale.Change(x: -scale.x);

            var camera = _playerFactory.CreateCamera();
            var cameraModel = new CameraModel()
            {
                Camera = camera,
                GameObject = camera.gameObject,
                Transform = camera.transform,
            };
            _camera = cameraModel;
            _camera.Transform.SetParent(null);
            _camera.Transform.position = _camera.Transform.position.Change(z: _config.CameraDistance);
        }

        public PlayerModel GetPlayer()
        {
            return _player;
        }
        
        public CameraModel GetCamera()
        {
            return _camera;
        }
    }
}