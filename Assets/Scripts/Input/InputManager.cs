using TheGame.GM;
using UnityEngine;

namespace TheGame.InputSystem
{
    public struct InputState
    {
        /// <summary>
        /// 按下
        /// </summary>
        public bool WasPressedThisFrame;
        
        /// <summary>
        /// 按着
        /// </summary>
        public bool WasPerformedThisFrame;
        
        /// <summary>
        /// 抬起
        /// </summary>
        public bool WasReleasedThisFrame;
        
        public Vector2 MousePosition;
    }

    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public InputState InputState;
        private InputStateMachine _inputStateMachine;

        private void Awake()
        {
            Instance = this;
        }

        public void Set(GameManager thisGame)
        {
            _inputStateMachine = new InputStateMachine(thisGame);
        }

        private void Update()
        {
            GetInputs();
            HandleInputs();
        }

        private void FixedUpdate()
        {
        }

        private void GetInputs()
        {
            InputState.WasPerformedThisFrame = Input.GetMouseButton(0);
            if (Input.GetMouseButtonDown(0))
                InputState.WasPressedThisFrame = true;
            if (Input.GetMouseButtonUp(0))
                InputState.WasReleasedThisFrame = true;
            
            InputState.MousePosition = Input.mousePosition;
        }

        private void HandleInputs()
        {
            _inputStateMachine.TickLogic(ref InputState);
            
            InputState.WasPressedThisFrame = false;
            InputState.WasReleasedThisFrame = false;
        }
    }
}