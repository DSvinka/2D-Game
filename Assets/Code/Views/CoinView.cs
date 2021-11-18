using System;
using Code.Interfaces.Views.Triggers;
using UnityEngine;

namespace Code.Views
{
    internal sealed class CoinView: MonoBehaviour, ITriggerEnterView
    {
        public event Action<GameObject, int> OnEnter = delegate(GameObject o, int i) {  };

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnter.Invoke(other.gameObject, gameObject.GetInstanceID());
        }
    }
}