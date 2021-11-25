using System;
using System.Collections.Generic;
using Code.Interfaces.Controllers;
using JetBrains.Annotations;

namespace Code
{
    internal sealed class GameControllers : IStart, IUpdate, ILateUpdate, ICleanup
    {
        private readonly List<IStart> _initializeControllers;
        private readonly List<IUpdate> _updateControllers;
        private readonly List<ILateUpdate> _lateControllers;
        private readonly List<ICleanup> _cleanupControllers;

        private readonly List<IController> _controllers;
        private readonly List<IInitialization> _initializations;

        internal GameControllers()
        {
            _initializeControllers = new List<IStart>(8);
            _updateControllers = new List<IUpdate>(8);
            _lateControllers = new List<ILateUpdate>(8);
            _cleanupControllers = new List<ICleanup>(8);
            
            _initializations = new List<IInitialization>(8);
            _controllers = new List<IController>(8);
        }

        public void Add(IController controller)
        {
            if (controller is IStart startController)
            {
                _initializeControllers.Add(startController);
            }

            if (controller is IUpdate updateController)
            {
                _updateControllers.Add(updateController);
            }

            if (controller is ILateUpdate lateUpdateController)
            {
                _lateControllers.Add(lateUpdateController);
            }

            if (controller is ICleanup cleanupController)
            {
                _cleanupControllers.Add(cleanupController);
            }

            _controllers.Add(controller);
        }
        
        public void Add(IInitialization initialization)
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

        public void Start()
        {
            for (var index = 0; index < _initializeControllers.Count; ++index)
            {
                _initializeControllers[index].Start();
            }
        }

        public void Update(float deltaTime)
        {
            for (var index = 0; index < _updateControllers.Count; ++index)
            {
                _updateControllers[index].Update(deltaTime);
            }
        }

        public void LateUpdate(float deltaTime)
        {
            for (var index = 0; index < _lateControllers.Count; ++index)
            {
                _lateControllers[index].LateUpdate(deltaTime);
            }
        }

        public void Cleanup()
        {
            for (var index = 0; index < _cleanupControllers.Count; ++index)
            {
                _cleanupControllers[index].Cleanup();
            }
        }
    }
}