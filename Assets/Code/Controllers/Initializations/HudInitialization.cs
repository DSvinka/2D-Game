using Code.Configs;
using Code.Factory;
using Code.Views;

namespace Code.Controllers.Initializations
{
    internal sealed class HudInitialization
    {
        private readonly HudFactory _hudFactory;
        private readonly HudConfig _hudConfig;
        
        private HudView _hudView;

        public HudInitialization(HudConfig hudConfig, HudFactory hudFactory)
        {
            _hudConfig = hudConfig;
            _hudFactory = hudFactory;
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