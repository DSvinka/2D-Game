using System;
using UnityEngine;

namespace Code.Interfaces.Views.Triggers
{
    internal interface ITriggerEnterView
    {
        event Action<GameObject, int> OnEnter;
    }
}