using System.Collections.Generic;
using Code.Interfaces.Controllers;
using Code.Models;
using Code.Utils.Modules;
using Code.Views;

namespace Code.Controllers
{
    internal sealed class EmitterController<T> : IController, IUpdate, ICleanup where T : IEmitter
    {
        private List<EmitterModel<T>> _emitterModels;
        private PoolService _poolService;

        public EmitterController(EmitterView[] emitterViews, T objectsController, PoolService poolService)
        {
            _emitterModels = new List<EmitterModel<T>>();
            _poolService = poolService;
            Setup(emitterViews, objectsController);
        }

        private void Setup(EmitterView[] emitterViews, T objectsController)
        {
            foreach (var emitterView in emitterViews)
            {
                _emitterModels.Add(new EmitterModel<T>()
                {
                    GameObject = emitterView.gameObject,
                    Transform = emitterView.transform,

                    Controller = objectsController,

                    Cooldown = 0,
                    Force = emitterView.Force,
                    Rate = emitterView.Rate,
                });
            }
        }

        public void ReSetup(EmitterView[] emitterViews, T objectsController)
        {
            Cleanup();
            Setup(emitterViews, objectsController);
        }

        public void Update(float deltaTime)
        {
            foreach (var emitterModel in _emitterModels)
            {
                if (emitterModel.Cooldown > 0)
                {
                    emitterModel.Cooldown -= deltaTime;
                }
                else
                {
                    emitterModel.Cooldown = emitterModel.Rate;
                    emitterModel.Controller.Trow(emitterModel.Transform.position,
                        -emitterModel.Transform.up * emitterModel.Force);
                }
            }
        }

        public void Cleanup()
        {
            _emitterModels.Clear();
        }
    }
}