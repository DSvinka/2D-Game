using System;
using UnityEngine;

namespace Code.Interfaces.Views.Triggers
{
    public interface ITriggerExitView
    {
        event Action<GameObject, int> OnExit;
    }
}