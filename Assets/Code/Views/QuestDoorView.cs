using System;
using Code.Interfaces.Views.Triggers;
using UnityEngine;

namespace Code.Views
{
    internal class QuestDoorView : MonoBehaviour, ITriggerQuestComplete
    {
        public event Action<int> OnQuestComplete = delegate(int i) {  };
        
        public void QuestComplete()
        {
            OnQuestComplete.Invoke(gameObject.GetInstanceID());
        }
    }
}