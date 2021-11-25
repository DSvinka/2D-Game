using System;
using System.Collections.Generic;
using Code.Interfaces.Controllers;
using Code.Managers;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    internal sealed class LevelChangerController: Controller
    {
        private readonly Dictionary<int, LevelChangerView> _levelChangers;

        public LevelChangerController()
        {
            _levelChangers = new Dictionary<int, LevelChangerView>();
        }
        
        public override void Setup(SceneViews sceneViews)
        {
            foreach (var changer in sceneViews.LevelChangerViews)
            {
                _levelChangers.Add(changer.gameObject.GetInstanceID(), changer);
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
            foreach (var changer in _levelChangers)
            {
                switch (changer.Value.TriggerType)
                {
                    case TriggerManager.Stay:
                        changer.Value.OnEnter += OnTriggerEnter;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(changer.Value.TriggerType), $"{changer.Value.TriggerType} не предусмотрен в этом коде");
                };
            }
        }

        public override void Cleanup()
        {
            foreach (var changer in _levelChangers)
            {
                var value = changer.Value;
                if (value == null || value.gameObject == null)
                    continue;

                switch (changer.Value.TriggerType)
                {
                    case TriggerManager.Stay:
                        changer.Value.OnEnter += OnTriggerEnter;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(changer.Value.TriggerType), $"{changer.Value.TriggerType} не предусмотрен в этом коде");
                };
                Object.Destroy(value.gameObject);
            }
            _levelChangers.Clear();
        }

        private void OnTriggerEnter(GameObject gameObject, int triggerID)
        {
            Debug.Log("Вы прошли уровень!");
            Debug.Log("Смены уровня нету...");
            gameObject.SetActive(false);
        }
    }
}