using Code.Configs;
using Code.Factory;
using Code.Interfaces.Controllers;
using Code.Views;
using UnityEngine;

namespace Code.Controllers.Initializations
{
    internal sealed class HudInitialization: IInitialization
    {
        private readonly HudFactory _hudFactory;
        private readonly HudConfig _hudConfig;
        
        private HudView _hudView;

        public HudInitialization(HudConfig hudConfig, HudFactory hudFactory)
        {
            _hudConfig = hudConfig;
            _hudFactory = hudFactory;
        }
        
        public void ReInitialization()
        {
            if (_hudView != null && _hudView.gameObject != null)
                Object.Destroy(_hudView.gameObject);
            
            Initialization();
        }

        public void Initialization()
        {
            var view = _hudFactory.CreateHud();
            _hudView = view;
        }

        public HudView GetHud()
        {
            return _hudView;
        }
    }
}