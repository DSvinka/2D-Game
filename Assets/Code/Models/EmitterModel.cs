using Code.Interfaces.Models;
using UnityEngine;

namespace Code.Models
{
    internal sealed class EmitterModel<T>: IGameObjectModel
    {
        public Transform Transform { get; set; }
        public GameObject GameObject { get; set; }
        
        public T Controller { get; set; }
        
        public float Cooldown { get; set; }
        public float Force { get; set; }
        public float Rate { get; set; }
    }
}