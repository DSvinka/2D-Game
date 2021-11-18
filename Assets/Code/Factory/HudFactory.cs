using System;
using Code.Configs;
using Code.Views;
using Object = UnityEngine.Object;

namespace Code.Factory
{
    internal sealed class HudFactory
    {
        private readonly HudConfig _hudConfig;

        public HudFactory(HudConfig hudConfig)
        {
            _hudConfig = hudConfig;
        }

        public HudView CreateHud()
        {
            var gameObject = Object.Instantiate(_hudConfig.Prefab);
            if (!gameObject.TryGetComponent(out HudView view))
                throw new Exception("У префаба интерфейса не найден компонент HudView!");
            return view;
        }
    }
}