using System.Collections.Generic;
using Common;
using MBF;
using UnityEngine;
using TheGame.CoreModule;
using TheGame.ResourceManagement;

namespace TheGame.GM
{
    public class CharacterManager : MonoBehaviour
    {
        private SceneVariants _sceneVariants;

        public void Set(SceneVariants sceneVariants)
        {
            _sceneVariants = sceneVariants;
        }

        public void LogicTick()
        {
            bool hasSomeOneDead = false;

            List<CharacterState> characters = _sceneVariants.characters;
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].IsDead)
                {
                    hasSomeOneDead = true;
                    RemoveCharacterAt_Internal(i);
                    continue;
                }

                characters[i].LogicTick();
            }

            if (hasSomeOneDead)
            {
                characters.RemoveNullElements();
            }
        }

        private void RemoveCharacterAt_Internal(int index)
        {
            List<CharacterState> characters = _sceneVariants.characters;
            Destroy(characters[index].gameObject);
            characters[index] = null;
        }
        
        public void RemoveCharacterAt(int index)
        {
            List<CharacterState> characters = _sceneVariants.characters;
            Destroy(characters[index].gameObject);
            characters[index] = null;
            characters.RemoveNullElements();
        }

        public void RemoveCharacter(CharacterState character)
        {
            int index = _sceneVariants.characters.IndexOf(character);
            if (index == -1) return;
            RemoveCharacterAt(index);
        }

        public CharacterState CreateCharacter(string id, int side, int grade, string[] tags = null)
        {
            LCharacterConfig cfg = LuaToCsBridge.CharacterTable[id];
            CharacterState cs = Instantiate(ResLoader.LoadAsset<GameObject>(PathHelper.GetPrefabPath($"Characters/{cfg.Prefab}"))).GetComponent<CharacterState>();

            cs.side = side;
            cs.grade = grade;
            cs.tags = tags;
            cs.id = id;

            List<EquipInfo> equipInfos = GameRuntimeData.Instance.EquipInfos;

            for (int i = 0; i < equipInfos.Count; i++)
            {
                if (equipInfos[i].chaId == id)
                {
                    cs.AddEquipment(new AddEquipmentInfo(LuaToCsBridge.EquipmentTable[equipInfos[i].itemId]));
                }
            }

            if (cfg.SkillIds is { Length: > 0 })
            {
                for (int i = 0; i < cfg.SkillIds.Length; i++)
                {
                    SkillModel skillModel = LuaToCsBridge.SkillTable[cfg.SkillIds[i]];
                    cs.LearnSkill(skillModel);
                }
            }

            ChaProp baseProp = DesignerFormula.GetGradeProp(cfg.BaseProp, cfg.PropGrowth[0], cfg.PropGrowth[1], grade);
            cs.InitBaseProp(baseProp);

            _sceneVariants.characters.Add(cs);

            return cs;
        }
    }
}