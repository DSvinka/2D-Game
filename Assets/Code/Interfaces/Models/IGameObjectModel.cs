using UnityEngine;

namespace Code.Interfaces.Models
{
    internal interface IGameObjectModel
    {
        Transform Transform { get; set; }
        GameObject GameObject { get; set; }
    }
}