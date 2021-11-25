using System;
using Code.Configs;
using Code.Views;
using Object = UnityEngine.Object;

namespace Code.Factory
{
    internal sealed class LevelFactory
    {
        private readonly LevelConfig _levelConfig;

        public LevelFactory(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public LevelView CreateLevel()
        {
            var gameObject = Object.Instantiate(_levelConfig.Prefab);
            if (!gameObject.TryGetComponent(out LevelView view))
                throw new Exception("У префаба интерфейса не найден компонент LevelView!");
            return view;
        }
    }
}