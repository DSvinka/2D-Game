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

        public GameInitialization(GameControllers controllers, ConfigStore config)
        {
            _controllers = controllers;
            _config = config;
        }

        public void Initialization()
        {
            var poolService = new PoolService();
            
            var sceneViews = FindGameObjects();
            var sceneFactories = SetupFactories();

            var sceneInitializations = SetupInitializations(sceneFactories);
            AddInitialization(sceneInitializations);
            
            var sceneControllers = SetupControllers(sceneViews, sceneInitializations, poolService);
            AddControllers(sceneControllers);
        }

        public SceneViews GetSceneViews()
        {
            return FindGameObjects();
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

        private SceneFactories SetupFactories()
        {
            var sceneFactories = new SceneFactories();
            
            sceneFactories.HudFactory = new HudFactory(_config.HudCfg);
            sceneFactories.PlayerFactory = new PlayerFactory(_config.PlayerCfg);
            sceneFactories.LevelFactory = new LevelFactory(_config.LevelGeneratorCfg);

            return sceneFactories;
        }

        private SceneInitializations SetupInitializations(SceneFactories sceneFactories)
        {
            var sceneInitializations = new SceneInitializations();
            
            sceneInitializations.InputInitialization = new InputInitialization();
            sceneInitializations.PlayerInitialization = new PlayerInitialization(_config.PlayerCfg, sceneFactories.PlayerFactory);
            sceneInitializations.HudInitialization = new HudInitialization(sceneFactories.HudFactory);
            sceneInitializations.LevelInitialization = new LevelInitialization(sceneFactories.LevelFactory);

            return sceneInitializations;
        }

        private SceneControllers SetupControllers(SceneViews sceneViews, SceneInitializations sceneInitializations, PoolService poolService)
        {
            var sceneControllers = new SceneControllers();
            var bulletController = new BulletController(_config.BulletCfg, poolService);
            var marchingSquaresController = new MarchingSquaresController();

            var playerInitialization = sceneInitializations.PlayerInitialization;
            var hudInitialization = sceneInitializations.HudInitialization;
            var levelInitialization = sceneInitializations.LevelInitialization;
            
            sceneControllers.InputController = new InputController();
            sceneControllers.LevelGeneratorController = new LevelGeneratorController(levelInitialization, _config.LevelGeneratorCfg, marchingSquaresController);
            sceneControllers.SpriteAnimatorController = new SpriteAnimatorController();
            sceneControllers.EmitterController = new EmitterController<BulletController>(bulletController, poolService);

            sceneControllers.HudController = new HudController(playerInitialization, hudInitialization);
            sceneControllers.CameraController = new CameraController(playerInitialization, _config.PlayerCfg);
            sceneControllers.PlayerController = new PlayerController(playerInitialization, _config.PlayerCfg);
            sceneControllers.PlayerMovementController = new PlayerMovementController(playerInitialization, sceneControllers.SpriteAnimatorController, _config.PlayerCfg);

            sceneControllers.EnemyController = new EnemyController(playerInitialization);
            sceneControllers.CoinController = new CoinController(sceneControllers.SpriteAnimatorController, _config.CoinAnimCfg);
            sceneControllers.CannonController = new CannonController( playerInitialization);
            sceneControllers.DamagerController = new DamagerController();
            sceneControllers.LevelChangerController = new LevelChangerController();

            return sceneControllers;
        }

        private void AddControllers(SceneControllers sceneControllers)
        {
            _controllers.Add(sceneControllers.InputController);
            _controllers.Add(sceneControllers.LevelGeneratorController);
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
            _controllers.Add(sceneInitializations.LevelInitialization);
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
        public LevelGeneratorController LevelGeneratorController;
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
        public LevelInitialization LevelInitialization;
        public PlayerInitialization PlayerInitialization;
        public HudInitialization HudInitialization;
    }
    
    internal struct SceneFactories
    {
        public PlayerFactory PlayerFactory;
        public HudFactory HudFactory;
        public LevelFactory LevelFactory;
    }
}