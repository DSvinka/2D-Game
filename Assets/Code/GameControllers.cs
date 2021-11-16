﻿using System.Collections.Generic;
using Code.Interfaces.Controllers;

namespace Code
{
    internal sealed class GameControllers : IStart, IUpdate, ILateUpdate, ICleanup
    {
        private readonly List<IStart> _initializeControllers;
        private readonly List<IUpdate> _executeControllers;
        private readonly List<ILateUpdate> _lateControllers;
        private readonly List<ICleanup> _cleanupControllers;

        internal GameControllers()
        {
            _initializeControllers = new List<IStart>(8);
            _executeControllers = new List<IUpdate>(8);
            _lateControllers = new List<ILateUpdate>(8);
            _cleanupControllers = new List<ICleanup>(8);
        }

        internal void Add(IController controller)
        {
            if (controller is IStart startController)
            {
                _initializeControllers.Add(startController);
            }

            if (controller is IUpdate updateController)
            {
                _executeControllers.Add(updateController);
            }

            if (controller is ILateUpdate lateUpdateController)
            {
                _lateControllers.Add(lateUpdateController);
            }

            if (controller is ICleanup cleanupController)
            {
                _cleanupControllers.Add(cleanupController);
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
            for (var index = 0; index < _executeControllers.Count; ++index)
            {
                _executeControllers[index].Update(deltaTime);
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