using Code.Interfaces.Models;
using Code.Views;
using UnityEngine;

namespace Code.Models
{
    internal sealed class CoinQuestModel: IQuestModel
    {
        public bool TryComplete(GameObject activator)
        {
            return activator.GetComponent<PlayerView>();
        }
    }
}