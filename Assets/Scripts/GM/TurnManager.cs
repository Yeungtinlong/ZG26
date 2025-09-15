using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MBF;
using UnityEngine;

namespace TheGame.GM
{
    public class TurnManager : MonoBehaviour
    {
        private const int k_ActionSpeed = 100;
        private const float k_AttackDuration = 2.0f;
        private const float k_MoveDuration = 0.2f;
        private const float k_TurnBreak = 1.0f;

        private int _currentTurn = 0;

        private SceneVariants _sceneVariants;
        private IEnumerator _turnCycle;

        public void Set(SceneVariants sceneVariants)
        {
            _sceneVariants = sceneVariants;
        }

        public void StartCycle()
        {
            StartCoroutine(TurnCycle());
        }

        private IEnumerator TurnCycle()
        {
            while (!CheckGameOver(out _))
            {
                _currentTurn++;
                yield return new WaitForSeconds(k_TurnBreak);
                yield return ActionCycle();
                yield return new WaitForSeconds(k_TurnBreak);

                _sceneVariants.characters.ForEach(cs => cs.ModifyRes(ChaResType.Speed, cs.Prop.speed));
            }
        }

        public bool CheckGameOver(out bool win)
        {
            if (_sceneVariants.characters.All(c => c.side == 0))
            {
                win = true;
                return true;
            }
            else if (_sceneVariants.characters.All(c => c.side == 1))
            {
                win = false;
                return true;
            }

            win = false;
            return false;
        }

        private IEnumerator ActionCycle()
        {
            while (!CheckGameOver(out _))
            {
                List<CharacterState> characters = new List<CharacterState>(_sceneVariants.characters);
                // 1. 为存活角色速度排序
                characters.Sort((c1, c2) => c2.GetResource(ChaResType.Speed) - c1.GetResource(ChaResType.Speed));
                CharacterState cs = characters.First();

                // 2. 行动
                if (cs.GetResource(ChaResType.Speed) < k_ActionSpeed)
                {
                    // NOTE: 行动循环结束
                    break;
                }

                cs.ModifyRes(ChaResType.Speed, -k_ActionSpeed);

                // 2.3. 攻击
                //TODO：目前凑效果做法是技能的AI，决定选中哪个目标。
                // 正确应该是有一个角色AI，角色AI应该站在了解所有技能效果的角度来使用技能，所以选中目标也应该是角色AI决定。
                cs.CastSkill(0);
                yield return new WaitForSeconds(k_MoveDuration + k_AttackDuration + k_MoveDuration);
            }
        }
    }
}