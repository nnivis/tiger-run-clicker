using UnityEngine;
using TigerClicker.CodeBase.Services.GameState;

namespace TigerClicker.CodeBase.Services.StateMachine
{
    public class GameState : StateMachineBehavior
    {
        [SerializeField] private GameStateHandler _gameStateHandler;
        protected override void OnEnter()
        {
            _gameStateHandler.StartWork();
        }

        protected override void OnExit()
        {
            _gameStateHandler.StopWork();
        }
    }
}
