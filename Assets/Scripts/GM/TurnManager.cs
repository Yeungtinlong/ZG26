using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MBF;
using UnityEngine;

namespace TheGame.GM
{
    public enum GameResult
    {
        Gaming,
        Win,
        Lose,
    }

    public class TurnManager : MonoBehaviour
    {
        private const int k_ActionSpeed = 100;
        private const float k_GameOverBreak = 1.0f;
        private const float k_TurnBreak = 0.5f;

        private int _currentTurn = 0;

        private SceneVariants _sceneVariants;
        private IEnumerator _turnCycle;
        private Action<GameResult> _onGameOver;

        private GameResult _gameResult;

        public void Set(SceneVariants sceneVariants, Action<GameResult> onGameOver)
        {
            _sceneVariants = sceneVariants;
            _onGameOver = onGameOver;
        }

        public void StartCycle()
        {
            _gameResult = GameResult.Gaming;
            StartCoroutine(TurnCycle());
        }

        private IEnumerator TurnCycle()
        {
            while (!CheckGameOver(out _gameResult))
            {
                _currentTurn++;
                yield return new WaitForSeconds(k_TurnBreak);
                yield return ActionCycle();
                yield return new WaitForSeconds(k_TurnBreak);

                _sceneVariants.characters.ForEach(cs => cs.ModifyRes(ChaResType.Speed, cs.Prop.speed));
            }

            _onGameOver?.Invoke(_gameResult);
        }

        public bool CheckGameOver(out GameResult gameResult)
        {
            if (_sceneVariants.characters.All(c => c.side == 0))
            {
                gameResult = GameResult.Win;
                return true;
            }
            else if (_sceneVariants.characters.All(c => c.side == 1))
            {
                gameResult = GameResult.Lose;
                return true;
            }

            gameResult = GameResult.Gaming;
            return false;
        }

        private IEnumerator ActionCycle()
        {
            while (!CheckGameOver(out _gameResult))
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
                yield return new WaitForSeconds(cs.skills[0].model.effect.duration * Time.fixedDeltaTime + k_TurnBreak);
            }
        }
    }
}