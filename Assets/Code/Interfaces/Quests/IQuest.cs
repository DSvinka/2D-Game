using System;
using Code.Interfaces.Controllers;

namespace Code.Interfaces.Quests
{
    internal interface IQuest: ICleanup
    {
        event EventHandler<IQuest> Completed;
        bool IsCompleted { get; }
        void Reset();
    }
}