using System;
using Code.Interfaces.Views.Triggers;
using Code.Managers;
using UnityEngine;

namespace Code.Views
{
    internal sealed class EnemyView : MonoBehaviour, ITriggerStayView
    {
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _damageRate = 2f;
        [SerializeField] private EnemyManager _enemyType;
        [SerializeField] private Transform[] _waypoint;

        public float Damage => _damage;
        public float DamageRate => _damageRate;
        public EnemyManager EnemyType => _enemyType;
        public Transform[] Waypoints => _waypoint;
        
        public event Action<GameObject, int> OnStay = delegate(GameObject obj, int damagerID) {  };
        
        private void OnCollisionStay2D(Collision2D other)
        {
            OnStay.Invoke(other.gameObject, gameObject.GetInstanceID());
        }
    }
}