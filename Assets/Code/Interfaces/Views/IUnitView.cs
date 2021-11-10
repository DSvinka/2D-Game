using System;
using UnityEngine;

namespace Code.Interfaces.Views
{
    internal interface IUnitView
    {
        event Action<GameObject, int, float> OnDamage;
        event Action<GameObject, int, float> OnHealing;

        void AddHealth(GameObject healer, float health);
        void AddDamage(GameObject attacker, float damage);
    }
}