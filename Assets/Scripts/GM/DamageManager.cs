using System.Collections.Generic;
using System.Linq;
using MBF;
using UnityEngine;

namespace TheGame.GM
{
    public sealed class DamageManager : MonoBehaviour
    {
        private readonly List<DamageInfo> _damageInfos = new List<DamageInfo>();
        private SceneVariants _sceneVariants;

        public void Set(SceneVariants sceneVariants)
        {
            _sceneVariants = sceneVariants;
        }
        
        public void CreateDamage(GameObject attacker, GameObject defender, Damage damage, DamageInfoTag[] tags)
        {
            _damageInfos.Add(new DamageInfo(attacker, defender, damage, tags));
        }
        
        public void LogicTick()
        {
            for (int i = 0; i < _damageInfos.Count; i++)
            {
                DamageInfo damageInfo = _damageInfos[i];
                if (damageInfo.defender == null || !damageInfo.defender.TryGetComponent(out CharacterState character) || character.IsDead)
                    continue;
                
                int damage = DesignerFormula.GetDamage(damageInfo);
                character.GetDamage(damage);
                
                Vector3 effectPos = damageInfo.defender.transform.position + new Vector3(Random.insideUnitCircle.x * 0.2f, Random.insideUnitCircle.y * 0.2f, 0f); 
                
                GameLuaInterface.PopText($"{Mathf.Abs(damage)}", 1f, damageInfo.tags.Contains(DamageInfoTag.DirectHurt) ? Color.white : Color.green, effectPos);
                
                _damageInfos[i] = null;
            }
            
            _damageInfos.Clear();
        }
    }
}