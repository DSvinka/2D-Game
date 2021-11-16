using System;
using System.Collections.Generic;
using Code.Interfaces.Controllers;
using Code.Managers;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class LevelChangersController: IController, IStart, ICleanup
    {
        private readonly Dictionary<int, LevelChangerView> _locationChanges;

        public LevelChangersController(IEnumerable<LevelChangerView> levelChangersViews)
        {
            _locationChanges = new Dictionary<int, LevelChangerView>();
            Setup(levelChangersViews);
        }
        
        private void Setup(IEnumerable<LevelChangerView> levelChangersViews)
        {
            foreach (var changer in levelChangersViews)
            {
                _locationChanges.Add(changer.gameObject.GetInstanceID(), changer);
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
            foreach (var changer in _locationChanges)
            {
                switch (changer.Value.TriggerType)
                {
                    case TriggerManager.Stay:
                        changer.Value.OnEnter += OnTriggerEnter;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"{changer.Value.TriggerType} не предусмотрен в этом коде");
                };
            }
        }

        public void Cleanup()
        {
            foreach (var changer in _locationChanges)
            {
                switch (changer.Value.TriggerType)
                {
                    case TriggerManager.Stay:
                        changer.Value.OnEnter += OnTriggerEnter;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"{changer.Value.TriggerType} не предусмотрен в этом коде");
                };
            }
            _locationChanges.Clear();
        }

        private void OnTriggerEnter(GameObject gameObject, int triggerID)
        {
            Debug.Log("Вы прошли уровень!");
            Debug.Log("Смены уровня нету...");
            gameObject.SetActive(false);
        }
    }
}