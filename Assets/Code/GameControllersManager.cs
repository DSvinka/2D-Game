using System;
using System.Collections.Generic;
using Code.Interfaces.Controllers;

namespace Code
{
    internal sealed class GameControllersManager : IInitialization, IExecute, ILateExecute, ICleanup
    {
        private readonly List<Controller> _controllers;
        private readonly List<Initializer> _initializations;

        internal GameControllersManager()
        {
            _controllers = new List<Controller>(8);
            _initializations = new List<Initializer>(8);
        }

        public void Add(Controller controller)
        {
            _controllers.Add(controller);
        }
        
        public void Add(Initializer initialization)
        {
            _initializations.Add(initialization);
        }

        public void Setup(SceneViews sceneViews)
        {
            for (var index = 0; index < _controllers.Count; ++index)
            {
                _controllers[index].Setup(sceneViews);
            }
            
            for (var index = 0; index < _initializations.Count; ++index)
            {
                _initializations[index].Initialization();
            }
        }
        
        public void Restart(SceneViews sceneViews)
        {
            for (var index = 0; index < _controllers.Count; ++index)
            {
                _controllers[index].ReSetup(sceneViews);
            }
            
            for (var index = 0; index < _initializations.Count; ++index)
            {
                _initializations[index].ReInitialization();
            }
        }

        public void Initialization()
        {
            for (var index = 0; index < _controllers.Count; ++index)
            {
                _controllers[index].Initialization();
            }
        }

        public void Execute(float deltaTime)
        {
            for (var index = 0; index < _controllers.Count; ++index)
            {
                _controllers[index].Execute(deltaTime);
            }
        }

        public void LateExecute(float deltaTime)
        {
            for (var index = 0; index < _controllers.Count; ++index)
            {
                _controllers[index].LateExecute(deltaTime);
            }
        }

        public void Cleanup()
        {
            for (var index = 0; index < _controllers.Count; ++index)
            {
                _controllers[index].Cleanup();
            }
        }
    }
}