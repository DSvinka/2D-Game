using UnityEngine;

namespace Code.Interfaces.Models
{
    internal interface IUnit
    {
        float Health { get; set; }
        bool IsGrounded { get; set; }
        bool LeftTurn { get; set; }
        
        Vector2 LeftScale { get; set; }
        Vector2 RightScale { get; set; }

        Transform Transform { get; set; }
        GameObject GameObject { get; set; }

        SpriteRenderer SpriteRenderer { get; set; }
        Collider2D Collider { get; set; }
        Rigidbody2D Rigidbody { get; set; }
    }
}