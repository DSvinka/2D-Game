using UnityEngine;

namespace Code.Interfaces.Models
{
    internal interface IUnit: IViewModel
    {
        float Health { get; set; }
        bool IsGrounded { get; set; }
        bool LeftTurn { get; set; }
        
        Vector2 LeftScale { get; set; }
        Vector2 RightScale { get; set; }
    }
}