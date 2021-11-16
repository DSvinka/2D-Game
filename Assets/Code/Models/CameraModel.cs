using Code.Interfaces.Models;
using UnityEngine;

namespace Code.Models
{
    internal sealed class CameraModel: IGameObjectModel
    {
        public Camera Camera { get; set; }
        
        public Transform Transform { get; set; }
        public GameObject GameObject { get; set; }
    }
}