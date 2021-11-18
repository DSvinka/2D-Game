using System;
using Code.Interfaces.Views.Triggers;
using UnityEngine;

namespace Code.Views
{
    internal sealed class BulletView : MonoBehaviour, ITriggerEnterView
    {
        public event Action<GameObject, int> OnEnter = delegate(GameObject o, int i) {  };

        public void OnTriggerEnter2D(Collider2D other)
        {
            OnEnter.Invoke(other.gameObject, gameObject.GetInstanceID());
        }
    }
}