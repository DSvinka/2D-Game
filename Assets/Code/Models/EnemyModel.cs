using Code.Interfaces.Models;
using Code.Managers;
using Code.Views;
using Pathfinding;
using UnityEngine;

namespace Code.Models
{
    internal sealed class EnemyModel : IGameObjectModel
    {
        public Transform Transform { get; set; }
        public GameObject GameObject { get; set; }
        
        public EnemyManager EnemyType { get; set; }
        public Transform[] Waypoints { get; set; }
        public int WaypointIndex { get; set; }
        
        public IAstarAI AStarAI { get; set; }
        public EnemyView EnemyView { get; set; }
        public float Cooldown { get; set; }

        public float DamageRate { get; set; }
        public float Damage { get; set; }
    }
}