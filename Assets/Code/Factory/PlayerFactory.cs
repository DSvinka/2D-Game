using System;
using Code.Configs;
using Code.Views;
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
            var gameObject = Object.Instantiate(_playerConfig.Prefab);
            if (!gameObject.TryGetComponent(out PlayerView view))
                throw new Exception("У префаба игрока не найден компонент PlayerView!");
            return view;
        }

    }
}