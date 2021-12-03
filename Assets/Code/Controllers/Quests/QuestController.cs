using System;
using Code.Interfaces.Controllers;
using Code.Interfaces.Models;
using Code.Interfaces.Quests;
using Code.Views;
using UnityEngine;

namespace Code.Controllers.Quests
{
    internal sealed class QuestController : ICleanup, IQuest
    {
        public event EventHandler<IQuest> Completed = delegate(object sender, IQuest quest) {  };
        public bool IsCompleted { get; private set; }

        private QuestObjectView _view;
        private IQuestModel _model;
        private bool _active;

        public QuestController(QuestObjectView view, IQuestModel model)
        {
            _view = view;
            _model = model;
        }

        private void OnCompleted()
        {
            Completed.Invoke(this, this);
        }

        private void OnContact(GameObject gameObject, int i)
        {
            var complete = _model.TryComplete(gameObject);
            if (complete)
                Complete();
        }

        private void Complete()
        {
            if (!_active)
                return;
            _active = false;
            IsCompleted = true;
            _view.OnEnter -= OnContact;
            _view.ProcessComplete();
            
            OnCompleted();
        }
        
        public void Reset()
        {
            if (_active)
                return;
            _active = true;
            IsCompleted = false;
            _view.OnEnter += OnContact;
            _view.ProcessActivate();
        }

        public void Cleanup()
        {
            _view.OnEnter -= OnContact;
        }
    }
}