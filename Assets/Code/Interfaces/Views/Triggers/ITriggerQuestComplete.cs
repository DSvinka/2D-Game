using System;

namespace Code.Interfaces.Views.Triggers
{
    internal interface ITriggerQuestComplete
    {
        event Action<int> OnQuestComplete;

        void QuestComplete();
    }
}