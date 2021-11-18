using Code.Interfaces.Models;
using Code.Views;
using UnityEngine;

namespace Code.Models
{
    internal sealed class CannonModel: IViewModel
    {
        public Transform Transform { get; set; }
        public GameObject GameObject { get; set; }
        
        public CannonView CannonView { get; set; }
        public Transform MuzzleTransform { get; set; }
        public Transform EmitterTransform { get; set; }
        
        public SpriteRenderer SpriteRenderer { get; set; }
        public Collider2D Collider { get; set; }
        public Rigidbody2D Rigidbody { get; set; }
    }
}