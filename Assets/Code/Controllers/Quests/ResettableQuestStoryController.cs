using System.Collections.Generic;
using System.Linq;
using Code.Interfaces.Controllers;
using Code.Interfaces.Quests;
using Code.Interfaces.Views.Triggers;
using UnityEngine;

namespace Code.Controllers.Quests
{
    internal sealed class ResettableQuestStoryController: ICleanup, IQuestStory
    {
        private readonly List<IQuest> _questCollection;
        private readonly ITriggerQuestComplete[] _triggersQuestComplete;
        public bool IsDone => _questCollection.All(value => value.IsCompleted);

        private int _currentIndex;

        public ResettableQuestStoryController(List<IQuest> questCollection, ITriggerQuestComplete[] triggersQuestComplete)
        {
            _triggersQuestComplete = triggersQuestComplete;
            _questCollection = questCollection;
            Subscribe();
            ResetQuest();
        }
        
        private void OnQuestCompleted(object sender, IQuest quest)
        {
            var index = _questCollection.IndexOf(quest);
            if (_currentIndex == index)
            {
                _currentIndex++;
                if (IsDone)
                {
                    foreach (var triggerQuestComplete in _triggersQuestComplete)
                    {
                        triggerQuestComplete.QuestComplete();
                    }
                }
            }
            else
            {
                ResetQuest();
            }
        }

        private void ResetQuest()
        {
            _currentIndex = 0;
            foreach (var quest in _questCollection)
            {
                quest.Reset();
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