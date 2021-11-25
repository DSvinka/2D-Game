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

        // Factory
        private PlayerFactory _playerFactory;
        private HudFactory _hudFactory;
        
        // Services
        private PoolService _poolService;

        public GameInitialization(GameControllers controllers, ConfigStore config)
        {
            _controllers = controllers;
            _config = config;

            _playerFactory = new PlayerFactory(_config.PlayerCfg);
            _hudFactory = new HudFactory(_config.HudCfg);
            
            _poolService = new PoolService();
            
            var sceneViews = FindGameObjects();

            var sceneInitializations = SetupInitializations();
            AddInitialization(sceneInitializations);
            
            var sceneControllers = SetupControllers(sceneViews, sceneInitializations);
            AddControllers(sceneControllers);
        }

        private SceneViews FindGameObjects()
        {
            var sceneViews = new SceneViews();
            
            sceneViews.EnemyViews = Object.FindObjectsOfType<EnemyView>();
            sceneViews.DamagerViews = Object.FindObjectsOfType<DamagerView>();
            sceneViews.LevelChangerViews = Object.FindObjectsOfType<LevelChangerView>();
            sceneViews.EmitterViews = Object.FindObjectsOfType<EmitterView>();
            sceneViews.CannonViews = Object.FindObjectsOfType<CannonView>();
            sceneViews.CoinViews = Object.FindObjectsOfType<CoinView>();

            return sceneViews;
        }

        private SceneInitializations SetupInitializations()
        {
            var sceneInitializations = new SceneInitializations();
            
            sceneInitializations.InputInitialization = new InputInitialization();
            sceneInitializations.PlayerInitialization = new PlayerInitialization(_config.PlayerCfg, _playerFactory);
            sceneInitializations.HudInitialization = new HudInitialization(_config.HudCfg, _hudFactory);

            return sceneInitializations;
        }

        private SceneControllers SetupControllers(SceneViews sceneViews, SceneInitializations sceneInitializations)
        {
            var sceneControllers = new SceneControllers();
            var bulletController = new BulletController(_config.BulletCfg, _poolService);

            var playerInitialization = sceneInitializations.PlayerInitialization;
            var hudInitialization = sceneInitializations.HudInitialization;
            
            sceneControllers.InputController = new InputController();
            sceneControllers.SpriteAnimatorController = new SpriteAnimatorController();
            sceneControllers.EmitterController = new EmitterController<BulletController>(sceneViews, bulletController, _poolService);

            sceneControllers.HudController = new HudController(playerInitialization, hudInitialization);
            sceneControllers.CameraController = new CameraController(playerInitialization, _config.PlayerCfg);
            sceneControllers.PlayerController = new PlayerController(playerInitialization, _config.PlayerCfg);
            sceneControllers.PlayerMovementController = new PlayerMovementController(playerInitialization, sceneControllers.SpriteAnimatorController, _config.PlayerCfg);

            sceneControllers.EnemyController = new EnemyController(sceneViews, playerInitialization);
            sceneControllers.CoinController = new CoinController(sceneViews, sceneControllers.SpriteAnimatorController, _config.CoinAnimCfg);
            sceneControllers.CannonController = new CannonController(sceneViews, playerInitialization);
            sceneControllers.DamagerController = new DamagerController(sceneViews);
            sceneControllers.LevelChangerController = new LevelChangerController(sceneViews);

            return sceneControllers;
        }

        private void AddControllers(SceneControllers sceneControllers)
        {
            _controllers.Add(sceneControllers.InputController);
            _controllers.Add(sceneControllers.SpriteAnimatorController);
            _controllers.Add(sceneControllers.EmitterController);
            
            _controllers.Add(sceneControllers.HudController);
            _controllers.Add(sceneControllers.CameraController);
            _controllers.Add(sceneControllers.PlayerController);
            _controllers.Add(sceneControllers.PlayerMovementController);

            _controllers.Add(sceneControllers.CoinController);
            _controllers.Add(sceneControllers.EnemyController);
            _controllers.Add(sceneControllers.DamagerController);
            _controllers.Add(sceneControllers.CannonController);
            _controllers.Add(sceneControllers.LevelChangerController);
        }
        
        private void AddInitialization(SceneInitializations sceneInitializations)
        {
            _controllers.Add(sceneInitializations.HudInitialization);
            _controllers.Add(sceneInitializations.PlayerInitialization);
        }
    }

    internal struct SceneViews
    {
        public EnemyView[] EnemyViews;
        public DamagerView[] DamagerViews;
        public LevelChangerView[] LevelChangerViews;
        public EmitterView[] EmitterViews;
        public CannonView[] CannonViews;
        public CoinView[] CoinViews;
    }

    internal struct SceneControllers
    {
        public InputController InputController;
        public SpriteAnimatorController SpriteAnimatorController;
        public EmitterController<BulletController> EmitterController;

        public HudController HudController;
        public CameraController CameraController;
        public PlayerController PlayerController;
        public PlayerMovementController PlayerMovementController;

        public CoinController CoinController;
        public EnemyController EnemyController;
        public DamagerController DamagerController;
        public CannonController CannonController;
        public LevelChangerController LevelChangerController;
    }

    internal struct SceneInitializations
    {
        public InputInitialization InputInitialization;
        public PlayerInitialization PlayerInitialization;
        public HudInitialization HudInitialization;
    }
}