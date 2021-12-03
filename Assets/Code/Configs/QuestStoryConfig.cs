using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "QuestStoryCfg", menuName = "Configs/Quest Story Cfg", order = 5)]
    internal sealed class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] Quests;
        public QuestStoryType QuestStoryType;
    }

    internal enum QuestStoryType
    {
        Common = 0,
        Resettable = 1,
    }
}