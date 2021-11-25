using System;
using Code.Configs;
using Code.Views;
using Object = UnityEngine.Object;

namespace Code.Factory
{
    internal sealed class LevelFactory
    {
        private readonly LevelGeneratorConfig _levelGeneratorConfig;

        public LevelFactory(LevelGeneratorConfig levelGeneratorConfig)
        {
            _levelGeneratorConfig = levelGeneratorConfig;
        }

        public LevelView CreateLevel()
        {
            var gameObject = Object.Instantiate(_levelGeneratorConfig.Prefab);
            if (!gameObject.TryGetComponent(out LevelView view))
                throw new Exception("У префаба интерфейса не найден компонент LevelView!");
            return view;
        }
    }
}