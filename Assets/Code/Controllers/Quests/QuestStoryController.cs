using System.Collections.Generic;
using System.Linq;
using Code.Interfaces.Controllers;
using Code.Interfaces.Quests;
using Code.Interfaces.Views.Triggers;
using UnityEngine;

namespace Code.Controllers.Quests
{
    internal sealed class QuestStoryController: ICleanup, IQuestStory
    {
        private readonly List<IQuest> _questCollection;
        private readonly ITriggerQuestComplete[] _triggersQuestComplete;
        
        public bool IsDone => _questCollection.All(value => value.IsCompleted);

        public QuestStoryController(List<IQuest> questCollection, ITriggerQuestComplete[] triggersQuestComplete)
        {
            _questCollection = questCollection;
            _triggersQuestComplete = triggersQuestComplete;
            Subscribe();
            ResetQuest(0);
        }
        
        private void OnQuestCompleted(object sender, IQuest quest)
        {
            var index = _questCollection.IndexOf(quest);
            if (IsDone)
            {
                foreach (var triggerQuestComplete in _triggersQuestComplete)
                {
                    triggerQuestComplete.QuestComplete();
                }
            }
            else
            {
                ResetQuest(++index);
            }
        }

        private void ResetQuest(int index)
        {
            if (index < 0 || index >= _questCollection.Count) return;

            var nextQuest = _questCollection[index];
            if (nextQuest.IsCompleted)
            {
                OnQuestCompleted(this, nextQuest);
            }
            else
            {
                _questCollection[index].Reset();
            }
        }

        private void Subscribe()
        {
            foreach (var quest in _questCollection)
            {
                quest.Completed += OnQuestCompleted;
            }
        }

        private void UnSubscribe()
        {
            foreach (var quest in _questCollection)
            {
                quest.Completed -= OnQuestCompleted;
            }
        }
            
        public void Cleanup()
        {
            UnSubscribe();
            foreach (var quest in _questCollection)
            {
                quest.Cleanup();
            }
            _questCollection.Clear();
        }
    }
}