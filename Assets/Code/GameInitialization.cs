using Code.Configs;
using Code.Controllers;
using Code.Controllers.Initializations;
using Code.Factory;
using Code.Utils.Modules;
using Code.Views;
using UnityEngine;

namespace Code
{
    internal sealed class GameInitialization
    {
        private GameControllers _controllers;
        private ConfigStore _config;

        // Initialization
        private InputInitialization _inputInitialization;
        private PlayerInitialization _playerInitialization;
        private HudInitialization _hudInitialization;
        
        // Controllers
        private InputController _inputController;
        private SpriteAnimatorController _spriteAnimatorController;

        private HudController _hudController;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private PlayerMovementController _playerMovementController;

        private CoinController _coinController;
        private BulletController _bulletController;
        private EmitterController<BulletController> _emitterController;

        private DamagerController _damagerController;
        private CannonController _cannonController;
        private LevelChangerController _levelChangerController;
        
        // Factory
        private PlayerFactory _playerFactory;
        private HudFactory _hudFactory;
        
        // Services
        private PoolService _poolService;
        
        // Views
        private DamagerView[] _damagerViews;
        private LevelChangerView[] _levelChangerViews;
        private EmitterView[] _emitterViews;
        private CannonView[] _cannonViews;
        private CoinView[] _coinViews;

        public GameInitialization(GameControllers controllers, ConfigStore config)
        {
            _controllers = controllers;
            _config = config;

            FindGameObjects();

            _playerFactory = new PlayerFactory(_config.PlayerCfg);
            _hudFactory = new HudFactory(_config.HudCfg);
            
            _poolService = new PoolService();

            SetupInitializations();
            SetupControllers();
            
            SaveControllers();
        }

        private void FindGameObjects()
        {
            _damagerViews = Object.FindObjectsOfType<DamagerView>();
            _levelChangerViews = Object.FindObjectsOfType<LevelChangerView>();
            _emitterViews = Object.FindObjectsOfType<EmitterView>();
            _cannonViews = Object.FindObjectsOfType<CannonView>();
            _coinViews = Object.FindObjectsOfType<CoinView>();
        }

        private void SetupInitializations()
        {
            _inputInitialization = new InputInitialization();
            _playerInitialization = new PlayerInitialization(_config.PlayerCfg, _playerFactory);
            _hudInitialization = new HudInitialization(_config.HudCfg, _hudFactory);
            
            _playerInitialization.Initialization();
            _hudInitialization.Initialization();
        }

        private void SetupControllers()
        {
            _inputController = new InputController();
            _spriteAnimatorController = new SpriteAnimatorController();

            _hudController = new HudController(_playerInitialization, _hudInitialization);
            _cameraController = new CameraController(_playerInitialization, _config.PlayerCfg);
            _playerController = new PlayerController(_playerInitialization, _config.PlayerCfg);
            _playerMovementController = new PlayerMovementController(_playerInitialization, _spriteAnimatorController, _config.PlayerCfg);
            
            _bulletController = new BulletController(_config.BulletCfg, _poolService);
            _emitterController = new EmitterController<BulletController>(_emitterViews, _bulletController, _poolService);
            
            _coinController = new CoinController(_coinViews, _spriteAnimatorController, _config.CoinAnimCfg);
            _cannonController = new CannonController(_playerInitialization, _cannonViews);
            _damagerController = new DamagerController(_damagerViews);
            _levelChangerController = new LevelChangerController(_levelChangerViews);
        }

        private void SaveControllers()
        {
            _controllers.Add(_inputController);
            _controllers.Add(_spriteAnimatorController);
            
            _controllers.Add(_hudController);
            _controllers.Add(_cameraController);
            _controllers.Add(_playerController);
            _controllers.Add(_playerMovementController);

            _controllers.Add(_emitterController);
            
            _controllers.Add(_coinController);
            _controllers.Add(_cannonController);
            _controllers.Add(_damagerController);
            _controllers.Add(_levelChangerController);
        }

        public void ReSetupControllers()
        {
            FindGameObjects();
            
            _playerInitialization.Initialization();
            _hudInitialization.Initialization();
            
            _spriteAnimatorController.ReSetup();
            _poolService.ReSetup();

            _hudController.ReSetup();
            _cameraController.ReSetup(_playerInitialization, _config.PlayerCfg);
            _playerController.ReSetup();
            _playerMovementController.ReSetup();
            
            _coinController.ReSetup(_coinViews);
            _cannonController.ReSetup(_cannonViews);
            _emitterController.ReSetup(_emitterViews, _bulletController);
            _damagerController.ReSetup(_damagerViews);
            _levelChangerController.ReSetup(_levelChangerViews);
        }
    }
}