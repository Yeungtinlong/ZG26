using MBF;
using UnityEngine;
using UnityHFSM;

namespace TheGame.InputSystem
{
    public class FreeState : StateBase<StateID>
    {
        private readonly Blackboard _blackboard;
        private readonly int _characterLayerMask;

        public FreeState(Blackboard blackboard) : base(false, false)
        {
            _blackboard = blackboard;
            _characterLayerMask = LayerMask.GetMask("Character");
        }

        public override void OnLogic()
        {
            InputState inputState = _blackboard.InputState;

            // 准备拖拽角色
            if (inputState.WasPressedThisFrame && !InputHelpers.IsPointerOverUIObject(inputState.MousePosition))
            {
                Vector2 worldPos = _blackboard.ThisGame.Camera.MainCamera.ScreenToWorldPoint(inputState.MousePosition);
                Collider2D collider2D = Physics2D.OverlapPoint(worldPos, _characterLayerMask);
                if (collider2D != null && collider2D.gameObject.TryGetComponent(out CharacterState cs) && cs.side == 0)
                {
                    _blackboard.ReadyDragPos = inputState.MousePosition;
                }
            }
            // 确认拖拽角色
            else if (inputState.WasPerformedThisFrame && !InputHelpers.IsPointerOverUIObject(inputState.MousePosition))
            {
                if ((inputState.MousePosition - _blackboard.ReadyDragPos).sqrMagnitude > 5f)
                {
                    Vector2 worldPos = _blackboard.ThisGame.Camera.MainCamera.ScreenToWorldPoint(inputState.MousePosition);
                    Collider2D collider2D = Physics2D.OverlapPoint(worldPos, _characterLayerMask);
                    if (collider2D != null && collider2D.gameObject.TryGetComponent(out CharacterState cs) && cs.side == 0)
                    {
                        _blackboard.draggingPawn = cs.gameObject;
                        _blackboard.LatestDragPos = _blackboard.ReadyDragPos;
                    }
                }
            }
        }
    }
}