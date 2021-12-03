using Code.Configs;
using UnityEngine;

namespace Code
{
    internal sealed class GameStarter : MonoBehaviour
    {
        [SerializeField] private ConfigStore _config;
        private GameControllersManager _gameControllersManager;

        private void Start()
        {
            _gameControllersManager = new GameControllersManager();
            
            var gameInitialization = new GameInitialization(_gameControllersManager, _config);
            var sceneViews = gameInitialization.GetSceneViews();
            gameInitialization.Initialization();

            _gameControllersManager.Setup(sceneViews);
            _gameControllersManager.Initialization();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _gameControllersManager.Execute(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _gameControllersManager.LateExecute(deltaTime);
        }

        private void OnDestroy()
        {
            _gameControllersManager.Cleanup();
        }
    }
}
