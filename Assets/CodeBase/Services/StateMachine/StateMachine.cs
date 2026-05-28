using System.Collections.Generic;
using UnityEngine;

namespace TigerClicker.CodeBase.Services.StateMachine
{
    public class StateMachine
    {
        private readonly List<IStateMachine> _states = new();
        private IStateMachine _currentState;

        public StateMachine(IEnumerable<IStateMachine> states)
        {
            _states.AddRange(states);
            _states.ForEach(x => x.Init(this));
        }

        public void Change<T>() where T : IStateMachine => Change(typeof(T));

        public void Change(System.Type typeNextState)
        {
            var nextState = _states.Find(x => x.GetType() == typeNextState);

            if (nextState == null)
            {
                Debug.LogError($"StateMachine: state {typeNextState.Name} not found");
                return;
            }

            if (_currentState == nextState) return;

            _currentState?.Exit();
            foreach (var view in _currentState?.GetViews() ?? new List<UnityEngine.GameObject>())
                view.SetActive(false);

            nextState.Enter();
            foreach (var view in nextState.GetViews())
                view.SetActive(true);

            _currentState = nextState;
        }

        public void Release()
        {
            _currentState?.Exit();
            _currentState = null;
            _states.Clear();
        }
    }
}
