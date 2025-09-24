using TheGame.GM;
using UnityEngine;
using UnityEngine.InputSystem;

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
        [SerializeField] private PlayerInput _playerInput;
        private InputAction _pressAction;
        private InputAction _releaseAction;
        private InputAction _moveAction;
        public static InputManager Instance { get; private set; }
        public InputState InputState;
        private InputStateMachine _inputStateMachine;
        
        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            EnableInputs();
        }

        private void OnDisable()
        {
            DisableInputs();
        }

        private void EnableInputs()
        {
            _pressAction = _playerInput.actions["Press"];
            _releaseAction = _playerInput.actions["Release"];
            _moveAction = _playerInput.actions["Move"];
            // _playerInput.ActivateInput();
            // _playerInput.actions.Enable();
        }

        private void DisableInputs()
        {
            // _playerInput.actions.Disable();
            // _playerInput.DeactivateInput();
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

        private void GetInputs()
        {
            if (_pressAction.triggered)
            {
                InputState.WasPressedThisFrame = true;
                InputState.WasPerformedThisFrame = true;
            }

            if (_releaseAction.triggered)
            {
                InputState.WasReleasedThisFrame = true;
                InputState.WasPerformedThisFrame = false;
            }

            InputState.MousePosition = _moveAction.ReadValue<Vector2>();
        }

        private void HandleInputs()
        {
            _inputStateMachine?.TickLogic(ref InputState);

            InputState.WasPressedThisFrame = false;
            InputState.WasReleasedThisFrame = false;
        }
    }
}