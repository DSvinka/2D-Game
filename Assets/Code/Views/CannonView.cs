using UnityEngine;

namespace Code.Views
{
    internal sealed class CannonView: MonoBehaviour
    {
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private Transform _emitterTransform;

        public Transform MuzzleTransform => _muzzleTransform;
        public Transform EmitterTransform => _emitterTransform;
    }
}