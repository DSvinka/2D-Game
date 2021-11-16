using System;
using UnityEngine;

namespace Code.Interfaces.Views.Triggers
{
    public interface ITriggerStayView
    {
        event Action<GameObject, int> OnStay;
    }
}