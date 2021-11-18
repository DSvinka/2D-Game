using Code.Interfaces.Models;
using UnityEngine;

namespace Code.Models
{
    internal sealed class PoolModel: IPoolModel
    {
        public Transform Transform { get; set; }
        public GameObject GameObject { get; set; }
        
        public SpriteRenderer SpriteRenderer { get; set; }
        public Collider2D Collider { get; set; }
        public Rigidbody2D Rigidbody { get; set; }
    }
}