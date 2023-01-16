using GameSystem.GameStates;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        private StateMachine _stateMachine;
        void Start()
        {
            _stateMachine = new StateMachine();
            _stateMachine.Register(States.Play, new PlayState());
            _stateMachine.Register(States.Start, new StartState());

            _stateMachine.InitialState = States.Start;
        }
    }
}
