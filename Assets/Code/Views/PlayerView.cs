using System;
using Code.Interfaces.Views;
using Code.Managers;
using UnityEngine;

namespace Code.Views
{
    internal class PlayerView : MonoBehaviour, IUnitView
    {
        public event Action<GameObject, int, float> OnDamage = delegate(GameObject attacker, int id, float damage) {  };
        public event Action<GameObject, int, float> OnHealing = delegate(GameObject healer, int id, float health) {  };
        public event Action<GameObject, int, PickupManager> OnPickup = delegate(GameObject item, int id, PickupManager pickupManager) {  };

        public void AddHealth(GameObject healer, float health)
        {
            OnHealing.Invoke(healer, gameObject.GetInstanceID(), health);
        }
        public void AddDamage(GameObject attacker, float damage)
        {
            OnDamage.Invoke(attacker, gameObject.GetInstanceID(), damage);
        }
        
        public void Pickup(GameObject item, PickupManager pickupManager)
        {
            OnPickup.Invoke(item, gameObject.GetInstanceID(), pickupManager);
        }
    }
}