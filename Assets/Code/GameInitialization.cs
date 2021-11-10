using Code.Configs;
using Code.Controllers;
using Code.Controllers.Initializations;
using Code.Factory;

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

            var playerFactory = new PlayerFactory(_config.PlayerCfg);

            var inputInitialization = new InputInitialization();
            var playerInitialization = new PlayerInitialization(config.PlayerCfg, playerFactory);
            playerInitialization.Initialization();

            var inputController = new InputController();
            var spriteAnimatorController = new SpriteAnimatorController();
            var playerController = new PlayerController(playerInitialization, config.PlayerCfg);
            var playerMovementController = new PlayerMovementController(playerInitialization, spriteAnimatorController, config.PlayerCfg);
            
            _controllers.Add(inputController);
            
            _controllers.Add(playerController);
            _controllers.Add(playerMovementController);
            _controllers.Add(spriteAnimatorController);
        }
    }
}