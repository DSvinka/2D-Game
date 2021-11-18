using System;
using Code.Interfaces.Views.Triggers;
using Code.Managers;
using UnityEngine;

namespace Code.Views
{
    internal sealed class LevelChangerView : MonoBehaviour, ITriggerEnterView
    {
        [SerializeField] private string _levelName;
        [SerializeField] private TriggerManager _triggerType;

        public string LevelName => _levelName;
        public TriggerManager TriggerType => _triggerType;
        public event Action<GameObject, int> OnEnter = delegate(GameObject o, int i) {  };

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnter.Invoke(other.gameObject, gameObject.GetInstanceID());
        }
    }
}