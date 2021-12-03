using System.Collections.Generic;
using Code.Configs;
using Code.Managers;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class QuestDoorController: Controller
    {
        private Dictionary<int, QuestDoorView> _questDoorViews;

        public QuestDoorController()
        {
            _questDoorViews = new Dictionary<int, QuestDoorView>();
        }

        public override void Setup(SceneViews sceneViews)
        {
            var questDoorViews = sceneViews.QuestDoorViews;
            foreach (var questDoorView in questDoorViews)
            {
                _questDoorViews.Add(questDoorView.gameObject.GetInstanceID(), questDoorView);
            }
        }
        public override void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Setup(sceneViews);
            Initialization();
        }
        
        public override void Initialization()
        {
            foreach (var questDoorView in _questDoorViews)
            {
                questDoorView.Value.OnQuestComplete += OnQuestComplete;
            }
        }
        
        public override void Cleanup()
        {
            foreach (var questDoorView in _questDoorViews)
            {
                questDoorView.Value.OnQuestComplete -= OnQuestComplete;
                if (questDoorView.Value != null)
                    Object.Destroy(questDoorView.Value.gameObject);
            }

            _questDoorViews.Clear();
        }

        private void OnQuestComplete(int questDoorViewID)
        {
            var questDoorView = _questDoorViews[questDoorViewID];
            questDoorView.gameObject.SetActive(false);
        }
    }
}