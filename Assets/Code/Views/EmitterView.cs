using UnityEngine;

namespace Code.Views
{
    internal sealed class EmitterView: MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private float _rate;
        
        public float Force => _force;
        public float Rate => _rate;
    }
}