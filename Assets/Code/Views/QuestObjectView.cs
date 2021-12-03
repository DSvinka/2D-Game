using System;
using Code.Interfaces.Views.Triggers;
using UnityEngine;

namespace Code.Views
{
    internal sealed class QuestObjectView : MonoBehaviour, ITriggerEnterView
    {
        [SerializeField] private int _id;
        [SerializeField] private Color _completedColor;
        [SerializeField] private Color _defaultColor;

        public int ID => _id;
        public event Action<GameObject, int> OnEnter = delegate(GameObject o, int i) {  };

        private void Awake()
        {
            _defaultColor = GetComponent<SpriteRenderer>().color;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnter.Invoke(other.gameObject, gameObject.GetInstanceID());
        }

        public void ProcessComplete()
        {
            GetComponent<SpriteRenderer>().color = _completedColor;
        }
        public void ProcessActivate()
        {
            GetComponent<SpriteRenderer>().color = _defaultColor;
        }
    }
}