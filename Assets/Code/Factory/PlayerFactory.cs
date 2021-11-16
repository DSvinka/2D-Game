using System;
using Code.Configs;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Factory
{
    internal sealed class PlayerFactory
    {
        private readonly PlayerConfig _playerConfig;

        public PlayerFactory(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }

        public PlayerView CreatePlayer()
        {
            var gameObject = Object.Instantiate(_playerConfig.PlayerPrefab);
            if (!gameObject.TryGetComponent(out PlayerView view))
                throw new Exception("У префаба игрока не найден компонент PlayerView!");
            return view;
        }

        public Camera CreateCamera()
        {
            var gameObject = Object.Instantiate(_playerConfig.CameraPrefab);
            if (!gameObject.TryGetComponent(out Camera camera))
                throw new Exception("У префаба камеры игрока не найден компонент Camera!");
            return camera;
        }
    }
}