using System.Collections.Generic;
using Code.Controllers.Initializations;
using Code.Models;
using UnityEngine;

namespace Code.Controllers.Enemies
{
    internal sealed class CannonController : Controller
    {
        private readonly List<CannonModel> _cannonModels;
        private readonly PlayerInitialization _playerInitialization;

        private PlayerModel _playerModel;

        public CannonController(PlayerInitialization playerInitialization)
        {
            _playerInitialization = playerInitialization;
            _cannonModels = new List<CannonModel>();
        }

        public override void Setup(SceneViews sceneViews)
        {
            var cannonViews = sceneViews.CannonViews;
            foreach (var cannonView in cannonViews)
            {
                _cannonModels.Add(new CannonModel()
                {
                    GameObject = cannonView.gameObject,
                    Transform = cannonView.transform,

                    CannonView = cannonView,
                    MuzzleTransform = cannonView.MuzzleTransform,
                    EmitterTransform = cannonView.EmitterTransform,

                    SpriteRenderer = cannonView.GetComponent<SpriteRenderer>(),
                    Rigidbody = cannonView.GetComponent<Rigidbody2D>(),
                    Collider = cannonView.GetComponent<Collider2D>(),
                });
            }
        }
        public override void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Setup(sceneViews);
            Initialization();
        }
        
        public override void Initialization()
        {
            _playerModel = _playerInitialization.GetPlayer();
        }

        public override void Execute(float deltaTime)
        {
            for (var index = 0; index < _cannonModels.Count; index++)
            {
                var cannonModel = _cannonModels[index];

                var direction = _playerModel.Transform.position - cannonModel.MuzzleTransform.position;
                var angle = Vector3.Angle(Vector3.down, direction);
                var axes = Vector3.Cross(Vector3.down, direction);

                cannonModel.MuzzleTransform.rotation = Quaternion.AngleAxis(angle, axes);
            }
        }

        public override void Cleanup()
        {
            foreach (var cannonModel in _cannonModels)
            {
                if (cannonModel != null && cannonModel.GameObject != null)
                    Object.Destroy(cannonModel.GameObject);
            }

            _cannonModels.Clear();
        }
    }
}