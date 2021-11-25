using Code.Configs;
using Code.Factory;
using Code.Interfaces.Controllers;
using Code.Views;
using UnityEngine;

namespace Code.Controllers.Initializations
{
    internal sealed class LevelInitialization: Initializer
    {
        private readonly LevelFactory _levelFactory;

        private LevelView _levelView;

        public LevelInitialization(LevelFactory levelFactory)
        {
            _levelFactory = levelFactory;
        }
        
        public override void Initialization()
        {
            var view = _levelFactory.CreateLevel();
            _levelView = view;
        }
        
        public override void ReInitialization()
        {
            if (_levelView != null && _levelView.gameObject != null)
                Object.Destroy(_levelView.gameObject);
            
            Initialization();
        }

        public LevelView GetLevel()
        {
            return _levelView;
        }
    }
}