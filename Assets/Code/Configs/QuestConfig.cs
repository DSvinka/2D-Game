using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "QuestCfg", menuName = "Configs/Quest Cfg", order = 5)]
    internal sealed class QuestConfig : ScriptableObject
    {
        public int ID;
        public QuestType QuestType;
    }

    public enum QuestType
    {
        Coins = 0,
    }
}