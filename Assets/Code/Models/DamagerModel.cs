using Code.Interfaces.Models;
using Code.Managers;
using Code.Views;
using UnityEngine;

namespace Code.Models
{
    internal sealed class DamagerModel : IGameObjectModel
    {
        public Transform Transform { get; set; }
        public GameObject GameObject { get; set; }

        public TriggerManager TriggerType { get; set; }
        public DamagerView DamagerView { get; set; }
        public float Cooldown { get; set; }

        public float DamageRate { get; set; }
        public float Damage { get; set; }
    }
}