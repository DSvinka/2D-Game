using Code.Configs;
using UnityEngine;

namespace Code
{
    internal sealed class GameStarter : MonoBehaviour
    {
        [SerializeField] private ConfigStore _config;
        private GameControllers _controllers;

        private void Start()
        {
            _controllers = new GameControllers();
            var gameInitialization = new GameInitialization(_controllers, _config);
            _controllers.Start();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Update(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateUpdate(deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.Cleanup();
        }
    }
}
