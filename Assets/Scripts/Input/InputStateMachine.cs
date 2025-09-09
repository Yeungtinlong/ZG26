using TheGame.GM;
using UnityEngine;
using UnityHFSM;

namespace TheGame.InputSystem
{
    public enum StateID
    {
        FreeState = 0,
        DraggingPawn,
    }

    public class Blackboard
    {
        public InputState InputState;
        public GameManager ThisGame;
        public GameObject draggingPawn;
        public Vector2 LatestDragPos;
        public Vector2 ReadyDragPos;
    }

    public class InputStateMachine
    {
        private StateMachine<StateID> _fsm;
        private readonly Blackboard _blackboard;

        public InputStateMachine(GameManager thisGame)
        {
            _blackboard = new Blackboard
            {
                ThisGame = thisGame,
            };

            _fsm = new StateMachine<StateID>();
            _fsm.AddState(StateID.FreeState, new FreeState(_blackboard));
            _fsm.AddState(StateID.DraggingPawn, new DraggingPawnState(_blackboard));

            _fsm.AddTransition(new Transition<StateID>(StateID.FreeState, StateID.DraggingPawn, t => _blackboard.draggingPawn != null));
            _fsm.AddTransition(new Transition<StateID>(StateID.DraggingPawn, StateID.FreeState, t => _blackboard.draggingPawn == null));

            _fsm.SetStartState(StateID.FreeState);
            _fsm.Init();
        }

        public void TickLogic(ref InputState inputState)
        {
            _blackboard.InputState = inputState;
            _fsm?.OnLogic();
        }
    }
}