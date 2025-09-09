using System.Collections.Generic;
using Common;
using MBF;
using TheGame.ResourceManagement;
using UnityEngine;

namespace TheGame.GM
{
    public sealed class AoeManager : MonoBehaviour
    {
        private SceneVariants _sceneVariants;

        public void Set(SceneVariants sceneVariants)
        {
            _sceneVariants = sceneVariants;
        }

        public void CreateAoe(AoeLauncher aoeLauncher)
        {
            AoeState aoe = Instantiate(ResLoader.LoadAsset<GameObject>($"Prefabs/Aoes/{aoeLauncher.model.prefab}.prefab"), aoeLauncher.targetPos, Quaternion.identity).GetComponent<AoeState>();
            aoe.InitByLauncher(aoeLauncher);
            aoe.side = aoeLauncher.side;

            _sceneVariants.aoes.Add(aoe);
        }

        public void RemoveAoe(AoeState aoeState, bool immediate = false)
        {
            aoeState.duration = 0;
            if (immediate)
            {
                aoeState.model.onRemove?.Invoke(aoeState);
                Destroy(aoeState.gameObject);
            }
        }

        public void LogicTick()
        {
            List<AoeState> aoes = _sceneVariants.aoes;
            List<CharacterState> characters = _sceneVariants.characters;
            bool hasAoeRemoved = false;
            for (int i = 0; i < aoes.Count; i++)
            {
                AoeState aoe = aoes[i];
                if (aoe == null)
                {
                    hasAoeRemoved = true;
                    continue;
                }

                // 先处理创造
                if (aoe.justCreated)
                {
                    aoe.justCreated = false;
                    aoe.model.onCreate?.Invoke(aoe, GetInRangeCharacters(aoe, characters));
                    continue;
                }

                // TODO: 处理移动

                // 更新范围内角色
                List<CharacterState> charactersInRange = GetInRangeCharacters(aoe, characters);

                // 处理离开
                if (aoe.model.onCharacterLeave != null)
                {
                    List<CharacterState> leaveCharacters = new List<CharacterState>();
                    for (int j = 0; j < aoe.charactersInRange.Count; j++)
                        // 离开：在上一帧中，但不在这帧中
                        if (!charactersInRange.Contains(aoe.charactersInRange[j]))
                            leaveCharacters.Add(aoe.charactersInRange[j]);

                    aoe.model.onCharacterLeave(aoe, leaveCharacters);
                }

                // 处理进入
                if (aoe.model.onCharacterEnter != null)
                {
                    List<CharacterState> enterCharacters = new List<CharacterState>();
                    for (int j = 0; j < charactersInRange.Count; j++)
                        // 进入：在这一帧中，但不在上一帧中
                        if (!aoe.charactersInRange.Contains(charactersInRange[j]))
                            enterCharacters.Add(charactersInRange[j]);

                    aoe.model.onCharacterEnter(aoe, enterCharacters);
                }

                // 更新范围内角色
                aoe.charactersInRange = charactersInRange;

                // 处理Tick
                if (aoe.tickTime != 0 && aoe.model.onTick != null && aoe.tickElapsed != 0 && aoe.tickElapsed % aoe.tickTime == 0)
                {
                    aoe.model.onTick(aoe);
                }

                // 处理生命周期
                aoe.tickElapsed++;
                aoe.duration--;

                if (aoe.duration <= 0)
                {
                    hasAoeRemoved = true;
                    if (aoe.model.onRemove != null)
                        aoe.model.onRemove(aoe);

                    Destroy(aoes[i].gameObject);
                    aoes[i] = null;
                }
            }

            if (hasAoeRemoved)
                aoes.RemoveNullElements();
        }

        private static List<CharacterState> GetInRangeCharacters(AoeState aoe, List<CharacterState> characters)
        {
            Vector3 pos = aoe.transform.position;
            float sqrRadius = aoe.radius * aoe.radius;
            List<CharacterState> result = new List<CharacterState>();

            for (int i = 0; i < characters.Count; i++)
            {
                if ((characters[i].transform.position - pos).sqrMagnitude <= sqrRadius)
                {
                    result.Add(characters[i]);
                }
            }

            return result;
        }

        private static void GetInRangeCharactersNonAlloc(AoeState aoe, List<CharacterState> allCharacters, ref List<CharacterState> result)
        {
            Vector3 pos = aoe.transform.position;
            float sqrRadius = aoe.radius * aoe.radius;
            for (int i = 0; i < allCharacters.Count; i++)
            {
                if ((allCharacters[i].transform.position - pos).sqrMagnitude <= sqrRadius)
                {
                    result.Add(allCharacters[i]);
                }
            }
        }
    }
}