using System.Collections.Generic;
using Code.Interfaces.Controllers;
using Code.Models;
using Code.Utils.Modules;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class EmitterController<T> : Controller where T : IEmitter
    {
        private List<EmitterModel<T>> _emitterModels;
        private T _objectsController;
        private PoolService _poolService;

        public EmitterController(T objectsController, PoolService poolService)
        {
            _emitterModels = new List<EmitterModel<T>>();
            _objectsController = objectsController;
            _poolService = poolService;
        }

        public override void Setup(SceneViews sceneViews)
        {
            var emitterViews = sceneViews.EmitterViews;
            foreach (var emitterView in emitterViews)
            {
                _emitterModels.Add(new EmitterModel<T>()
                {
                    GameObject = emitterView.gameObject,
                    Transform = emitterView.transform,

                    Controller = _objectsController,

                    Cooldown = 0,
                    Force = emitterView.Force,
                    Rate = emitterView.Rate,
                });
            }
        }
        public override void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Setup(sceneViews);
        }

        public override void Execute(float deltaTime)
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

        public override void Cleanup()
        {
            foreach (var emitterModel in _emitterModels)
            {
                if (emitterModel != null && emitterModel.GameObject != null)
                    Object.Destroy(emitterModel.GameObject);
            }
                
            _emitterModels.Clear();
        }
    }
}