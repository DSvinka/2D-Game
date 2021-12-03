using Code.Configs;
using Code.Factory;
using Code.Interfaces.Controllers;
using Code.Views;
using UnityEngine;

namespace Code.Controllers.Initializations
{
    internal sealed class HudInitialization: Initializer
    {
        private readonly HudFactory _hudFactory;

        private HudView _hudView;

        public HudInitialization(HudFactory hudFactory)
        {
            _hudFactory = hudFactory;
        }
        
        public override void Initialization()
        {
            var view = _hudFactory.CreateHud();
            _hudView = view;
        }
        public override void ReInitialization()
        {
            if (_hudView != null && _hudView.gameObject != null)
                Object.Destroy(_hudView.gameObject);
            
            Initialization();
        }

        public HudView GetHud()
        {
            return _hudView;
        }
    }
}