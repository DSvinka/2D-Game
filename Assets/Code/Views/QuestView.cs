using Code.Configs;
using Code.Interfaces.Views.Triggers;
using UnityEngine;

namespace Code.Views
{
    internal sealed class QuestView : MonoBehaviour
    {
        [SerializeField] private QuestObjectView _singleQuest;
        [SerializeField] private QuestStoryConfig[] _questStoryConfigs;
        [SerializeField] private QuestObjectView[] _questsObjects;

        [SerializeField] private QuestDoorView[] _triggersQuestComplete;


        public QuestObjectView SingleQuest => _singleQuest;
        public QuestStoryConfig[] QuestStoryConfigs => _questStoryConfigs;
        public QuestObjectView[] QuestsObjects => _questsObjects;
        
        public QuestDoorView[] TriggersQuestComplete => _triggersQuestComplete;
    }
}