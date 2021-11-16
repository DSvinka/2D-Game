using Code.Managers;

namespace Code.Interfaces.Views.Triggers
{
    internal interface ITriggerView: ITriggerEnterView, ITriggerExitView, ITriggerStayView
    {
        TriggerManager TriggerType { get; }
    }
}