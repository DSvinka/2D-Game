using UnityEngine;

namespace Code.Interfaces.Models
{
    internal interface IQuestModel
    {
        bool TryComplete(GameObject activator);
    }
}