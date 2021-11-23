using System;
using Code.Interfaces.Views.Triggers;
using Code.Managers;
using UnityEngine;

namespace Code.Views
{
    internal sealed class DamagerView : MonoBehaviour, ITriggerView
    {
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _damageRate = 2f;
        [SerializeField] private TriggerManager _triggerType;

        public float Damage => _damage;
        public float DamageRate => _damageRate;

        public TriggerManager TriggerType => _triggerType;
        public event Action<GameObject, int> OnEnter = delegate(GameObject obj, int damagerID) {  };
        public event Action<GameObject, int> OnStay = delegate(GameObject obj, int damagerID) {  };
        public event Action<GameObject, int> OnExit = delegate(GameObject obj, int damagerID) {  };

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnter.Invoke(other.gameObject, gameObject.GetInstanceID());
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            OnStay.Invoke(other.gameObject, gameObject.GetInstanceID());
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            OnExit.Invoke(other.gameObject, gameObject.GetInstanceID());
        }
    }
}