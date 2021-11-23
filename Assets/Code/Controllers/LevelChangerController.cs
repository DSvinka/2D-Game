using System;
using System.Collections.Generic;
using Code.Interfaces.Controllers;
using Code.Managers;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class LevelChangerController: IController, IStart, ICleanup
    {
        private readonly Dictionary<int, LevelChangerView> _levelChangers;

        public LevelChangerController(IEnumerable<LevelChangerView> levelChangersViews)
        {
            _levelChangers = new Dictionary<int, LevelChangerView>();
            Setup(levelChangersViews);
        }
        
        private void Setup(IEnumerable<LevelChangerView> levelChangersViews)
        {
            foreach (var changer in levelChangersViews)
            {
                _levelChangers.Add(changer.gameObject.GetInstanceID(), changer);
            }
        }

        public void ReSetup(IEnumerable<LevelChangerView> levelChangersViews)
        {
            Cleanup();
            Setup(levelChangersViews);
            Start();
        }

        public void Start()
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

        public void Cleanup()
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