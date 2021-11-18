using System.Collections.Generic;
using Code.Controllers.Initializations;
using Code.Interfaces.Controllers;
using Code.Models;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class CannonController : IController, IUpdate, IStart, ICleanup
    {
        private readonly List<CannonModel> _cannonModels;
        private readonly PlayerInitialization _playerInitialization;

        private PlayerModel _playerModel;

        public CannonController(PlayerInitialization playerInitialization, CannonView[] cannonViews)
        {
            _playerInitialization = playerInitialization;
            _cannonModels = new List<CannonModel>();
            Setup(cannonViews);
        }

        private void Setup(CannonView[] cannonViews)
        {
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

        public void ReSetup(CannonView[] cannonViews)
        {
            Cleanup();
            Setup(cannonViews);
            Start();
        }
        
        public void Start()
        {
            _playerModel = _playerInitialization.GetPlayer();
        }

        public void Update(float deltaTime)
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

        public void Cleanup()
        {
            _cannonModels.Clear();
        }
    }
}