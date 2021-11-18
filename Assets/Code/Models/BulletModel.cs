using Code.Interfaces.Models;
using Code.Views;
using UnityEngine;

namespace Code.Models
{
    internal sealed class BulletModel: IViewModel
    {
        public Transform Transform { get; set; }
        public GameObject GameObject { get; set; }
        
        public float Damage { get; set; }
        public BulletView BulletView { get; set; }
        
        public SpriteRenderer SpriteRenderer { get; set; }
        public Collider2D Collider { get; set; }
        public Rigidbody2D Rigidbody { get; set; }
    }
}