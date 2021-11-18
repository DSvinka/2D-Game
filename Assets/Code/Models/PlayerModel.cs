using Code.Interfaces.Models;
using Code.Views;
using UnityEngine;

namespace Code.Models
{
    internal sealed class PlayerModel: IUnit
    {
        public float Health { get; set; }
        public int Coins { get; set; }
        
        public bool IsGrounded { get; set; }
        public bool LeftTurn { get; set; }
        
        public Vector2 LeftScale { get; set; }
        public Vector2 RightScale { get; set; }
        
        public PlayerView View { get; set; }
        public Transform Transform { get; set; }
        public GameObject GameObject { get; set; }

        public SpriteRenderer SpriteRenderer { get; set; }
        public Collider2D Collider { get; set; }
        public Rigidbody2D Rigidbody { get; set; }
    }
}