using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Interfaces.Models;
using Code.Interfaces.Quests;
using Code.Interfaces.Views.Triggers;
using Code.Models;
using Code.Views;
using UnityEngine;

namespace Code.Controllers.Quests
{
    internal sealed class QuestConfiguratorController: Controller
    {
        private QuestObjectView _singleQuestView;
        private QuestController _singleQuest;
        private CoinQuestModel _model;

        private QuestStoryConfig[] _questStoryConfigs;
        private QuestObjectView[] _questObjectViews;
        private ITriggerQuestComplete[] _triggersQuestComplete;

        private List<IQuestStory> _questStories;
        private readonly Dictionary<QuestType, Func<IQuestModel>> _questFactories;
        private readonly Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories;

        public QuestConfiguratorController()
        {
            _model = new CoinQuestModel();
            _questFactories = new Dictionary<QuestType, Func<IQuestModel>>(1);
            _questStoryFactories = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>(2);
        }

        public override void Setup(SceneViews sceneViews)
        {
            var questView = sceneViews.QuestView;
            
            _singleQuestView = questView.SingleQuest;
            _questStoryConfigs = questView.QuestStoryConfigs;
            _questObjectViews = questView.QuestsObjects;
            _triggersQuestComplete = questView.TriggersQuestComplete;
        }
        public override void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Setup(sceneViews);
        }
        public override void Cleanup()
        {
            _questFactories.Clear();
            _questStoryFactories.Clear();
            _questStories.Clear();
        }

        public override void Initialization()
        {
            _singleQuest = new QuestController(_singleQuestView, _model);
            _singleQuest.Reset();
            
            _questStoryFactories.Add(QuestStoryType.Common, questCollection => new QuestStoryController(questCollection, _triggersQuestComplete));
            _questStoryFactories.Add(QuestStoryType.Resettable, questCollection => new ResettableQuestStoryController(questCollection, _triggersQuestComplete));
            
            _questFactories.Add(QuestType.Coins, () => new CoinQuestModel());

            _questStories = new List<IQuestStory>();
            foreach (var questStoryConfig in _questStoryConfigs)
            {
                _questStories.Add(CreateQuestStory(questStoryConfig));
            }
        }

        private IQuestStory CreateQuestStory(QuestStoryConfig config)
        {
            var quests = new List<IQuest>();

            foreach (var questConfig in config.Quests)
            {
                var quest = CreateQuest(questConfig);
                if (quest == null) continue;
                
                quests.Add(quest);
            }

            return _questStoryFactories[config.QuestStoryType].Invoke(quests);
        }

        private IQuest CreateQuest(QuestConfig questConfig)
        {
            var questID = questConfig.ID;
            var questView = _questObjectViews.FirstOrDefault(value => value.ID == questID);
            if (questView == null)
            {
                Debug.Log($"View квеста {questID} не найден!");
                return null;
            }

            if (_questFactories.TryGetValue(questConfig.QuestType, out var factory))
            {
                var questModel = factory.Invoke();
                return new QuestController(questView, questModel);
            }

            Debug.Log($"Model квеста {questID} не найден!");
            return null;
        }
    }
}