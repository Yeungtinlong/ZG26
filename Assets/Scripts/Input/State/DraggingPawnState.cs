using MBF;
using TheGame.GM;
using UnityEngine;
using UnityHFSM;

namespace TheGame.InputSystem
{
    public class DraggingPawnState : StateBase<StateID>
    {
        private readonly Blackboard _blackboard;
        private readonly int _characterLayerMask;
        private readonly int _mapGridLayerMask;

        public DraggingPawnState(Blackboard blackboard) : base(false, false)
        {
            _blackboard = blackboard;
            _characterLayerMask = LayerMask.GetMask("Character");
            _mapGridLayerMask = LayerMask.GetMask("MapGrid");
        }

        public override void OnLogic()
        {
            InputState inputState = _blackboard.InputState;

            if (_blackboard.draggingPawn != null && inputState.WasPerformedThisFrame)
            {
                Vector2 currentPos = inputState.MousePosition;
                Vector2 vec = (currentPos - _blackboard.LatestDragPos);
                Vector2 dir = vec.normalized;
                float length = vec.magnitude;

                Vector2 worldPos = _blackboard.ThisGame.Camera.MainCamera.ScreenToWorldPoint(inputState.MousePosition);
                Collider2D collider2D = Physics2D.OverlapPoint(worldPos, _characterLayerMask);
                if (collider2D != null && collider2D.gameObject.TryGetComponent(out CharacterState cs) && cs.side == 0)
                {
                    _blackboard.ReadyDragPos = inputState.MousePosition;
                }

                _blackboard.draggingPawn.transform.position = worldPos;
                _blackboard.ThisGame.Camera.MoveCameraVec(-dir * (length * _blackboard.ThisGame.Camera.MainCamera.orthographicSize * 2f / Screen.height));
                _blackboard.LatestDragPos = currentPos;
            }
            // 尝试摆放
            else if (_blackboard.draggingPawn != null && inputState.WasReleasedThisFrame)
            {
                CharacterState draggingCharacter = _blackboard.draggingPawn.GetComponent<CharacterState>();
                MapGrid draggingCharacterGrid = draggingCharacter.Grid;
                
                Vector2 worldPos = _blackboard.ThisGame.Camera.MainCamera.ScreenToWorldPoint(inputState.MousePosition);
                Collider2D collider2D = Physics2D.OverlapPoint(worldPos, _mapGridLayerMask);
                // 如果落在Grid上，则进行“交换”或者“放置”
                if (collider2D != null && collider2D.gameObject.TryGetComponent(out MapGrid dstGrid) && dstGrid.Side == 0)
                {
                    CharacterState dstCharacter = dstGrid.Character;
                    // 如果Grid原本有角色，则交换
                    if (dstCharacter != null)
                    {
                        _blackboard.ThisGame.AddCharacterToGrid(draggingCharacter, dstGrid);
                        _blackboard.ThisGame.AddCharacterToGrid(dstCharacter, draggingCharacterGrid);
                    }
                    else
                    {
                        _blackboard.ThisGame.AddCharacterToGrid(draggingCharacter, dstGrid);
                    }
                }
                else
                {
                    // 返回原地
                    _blackboard.ThisGame.AddCharacterToGrid(draggingCharacter, draggingCharacterGrid);
                }

                _blackboard.draggingPawn = null;
            }
        }
    }
}