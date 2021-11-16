using UnityEngine;

namespace Code.Interfaces.Models
{
    internal interface IViewModel: IGameObjectModel
    {
        SpriteRenderer SpriteRenderer { get; set; }
        Collider2D Collider { get; set; }
        Rigidbody2D Rigidbody { get; set; }
    }
}