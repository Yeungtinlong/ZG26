using System;
using System.Collections.Generic;
using Common;
using MBF.UnitBehaviors;
using UnityEngine;
using TheGame;
using TheGame.GM;
using TheGame.ResourceManagement;

namespace MBF
{
    public class CharacterState : MonoBehaviour, IBeAttacked
    {
        public string id;

        public string[] tags;

        public bool IsDead { get; private set; }
        public int Hp => _resource.hp;

        public int grade;
        public int side;

        /// <summary>
        /// 当前属性
        /// </summary>
        private ChaProp _prop = ChaProp.zero;

        public ChaProp Prop => _prop;

        /// <summary>
        /// 基础属性，角色裸体属性，从表中获取
        /// </summary>
        public ChaProp baseProp;

        /// <summary>
        /// Buff带来的属性，
        /// [0]: buff plus
        /// [1]: buff times
        /// </summary>
        public ChaProp[] buffProps = new ChaProp[2];

        /// <summary>
        /// 装备带来的属性，计算方式同上
        /// </summary>
        public ChaProp[] equipmentProps = new ChaProp[2];

        private ChaRes _resource;
        public ChaRes resource => _resource;

        public List<BuffObj> buffs = new List<BuffObj>();
        public EquipmentObj[] equipments = new EquipmentObj[ChaInstance.EQUIP_LIMIT];
        public List<SkillObj> skills = new List<SkillObj>();

        public ChaControlState controlState = ChaControlState.origin;
        public Vector3 _moveOrder = Vector3.zero;

        public MapGrid Grid { get; set; }

        public Action<CharacterState> OnDie;
        public Action<CharacterState> OnResourceChanged;

        private UnitMove _unitMove;
        private UnitAnim _unitAnim;
        private UnitViewController _unitViewController;

        private void Awake()
        {
            SyncUnitBehaviors();
        }

        public void InitBaseProp(ChaProp cProp)
        {
            this.baseProp = cProp;
            RecheckProps();
            SetResource(new ChaRes(_prop.hp, _prop.speed, 0));
            OnResourceChanged?.Invoke(this);

            SyncUnitBehaviors();
        }

        private void SyncUnitBehaviors()
        {
            _unitMove = GetComponent<UnitMove>();
            _unitViewController = GetComponentInChildren<UnitViewController>();
            _unitAnim = GetComponent<UnitAnim>();
        }

        private void RecheckProps()
        {
            _prop.Zero();
            controlState.Origin();

            for (int i = 0; i < buffProps.Length; i++)
                buffProps[i].Zero();

            for (int i = 0; i < equipmentProps.Length; i++)
                equipmentProps[i].Zero();

            for (int i = 0; i < buffs.Count; i++)
            {
                for (int j = 0; j < Mathf.Min(2, buffs[i].model.propMod.Length); j++)
                    buffProps[j] += buffs[i].model.propMod[j];

                controlState += buffs[i].model.stateMod;
            }

            for (int i = 0; i < equipments.Length; i++)
            {
                if (equipments[i] == null) continue;

                for (int j = 0; j < Mathf.Min(2, equipments[i].model.propMod.Length); j++)
                    equipmentProps[j] += equipments[i].model.propMod[j];
            }

            _prop = ((baseProp + equipmentProps[0]) * equipmentProps[1] + buffProps[0]) * buffProps[1];
        }

        public void AddBuff(AddBuffInfo info, bool recheck = true)
        {
            bool hasSameBuff = false;
            for (int i = 0; i < buffs.Count; i++)
            {
                if (buffs[i].model.id == info.model.id)
                {
                    buffs[i].duration = Mathf.Max(buffs[i].duration, info.duration);
                    hasSameBuff = true;
                }
            }

            if (hasSameBuff)
                return;

            BuffObj buffObj = new BuffObj(info.model, info.caster, info.duration, info.permanent);
            buffs.Add(buffObj);

            if (recheck)
                RecheckProps();
        }

        public void AddEquipment(AddEquipmentInfo info)
        {
            EquipmentObj equipmentObj = new EquipmentObj(info.model);

            if (equipmentObj.model.addBuffs != null)
                for (int i = 0; i < equipmentObj.model.addBuffs.Length; i++)
                    AddBuff(equipmentObj.model.addBuffs[i], false);

            equipments[(int)equipmentObj.model.slot] = equipmentObj;
            RecheckProps();
        }

        public void LearnSkill(SkillModel model)
        {
            skills.Add(new SkillObj(model, 1));
        }

        public void CastSkill(int idx)
        {
            if (idx < 0 || idx >= skills.Count) return;
            SkillObj skillObj = skills[idx];
            GameLuaInterface.CreateTimeline(new TimelineObj(skillObj.model.effect, gameObject, skillObj));
            // if (skillObj.cooldown > 0) return;
            // float skillSpd = DesignerFormula.GetSkillSpeed(_prop.skillSpd);
            // skillObj.cooldown = Mathf.RoundToInt(skillObj.model.cooldown / skillSpd);
        }

        // public void Upgrade()
        // {
        //     grade++;
        //     LCharacterConfig cfg = LuaToCsBridge.CharacterTable[id];
        //     baseProp = DesignerFormula.GetGradeProp(cfg.BaseProp, cfg.PropGrowth[0], cfg.PropGrowth[1], grade);
        //     UpgradeSkills();
        //     RecheckProps();
        //     SetResource(new ChaRes(_prop.hp, _resource.speed, 0));
        //     OnResourceChanged?.Invoke(this);
        // }

        public void SetResource(ChaRes res)
        {
            _resource = res;
            OnResourceChanged?.Invoke(this);
        }

        public void SetResource(ChaResType type, int value)
        {
            switch (type)
            {
                case ChaResType.Health:
                {
                    _resource.hp = value;
                    break;
                }
                case ChaResType.Speed:
                {
                    _resource.speed = value;
                    break;
                }
                case ChaResType.Shield:
                {
                    _resource.shp = value;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            OnResourceChanged?.Invoke(this);
        }

        public void ModifyRes(ChaResType type, int delta)
        {
            SetResource(type, GetResource(type) + delta);
        }

        public int GetResource(ChaResType type)
        {
            switch (type)
            {
                case ChaResType.Health:
                {
                    return _resource.hp;
                }
                case ChaResType.Speed:
                {
                    return _resource.speed;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        // public void UpgradeSkills()
        // {
        //     for (int i = 0; i < skills.Count; i++)
        //     {
        //         skills[i].grade++;
        //     }
        // }

        public void LogicTick()
        {
            bool needRecheckProps = false;

            // 处理Buff
            for (int i = 0; i < buffs.Count; i++)
            {
                buffs[i].duration--;
                if (buffs[i].duration <= 0)
                {
                    needRecheckProps = true;
                    buffs[i] = null;
                }
            }

            buffs.RemoveNullElements();
            if (needRecheckProps)
                RecheckProps();

            // 处理技能冷却
            for (int i = 0; i < skills.Count; i++)
            {
                skills[i].cooldown = Mathf.Max(0, skills[i].cooldown - 1);
            }

            if (!controlState.canMove)
                _moveOrder = Vector3.zero;

            // 处理移动
            bool tryMove = _moveOrder != Vector3.zero && controlState.canMove;
            Vector2 tryMoveDir = tryMove ? _moveOrder : Vector2.zero;
            // TODO: _moveOrder应该是来自所有移动信息的总和，移动信息应该使用List<MovePreorder>储存，这样就可以区分开玩家主动的移动和被技能效果影响的移动。

            _unitMove.MoveBy(_moveOrder);
            _moveOrder = Vector3.zero;

            if (tryMove)
            {
                _unitAnim.Play("move");
                _unitAnim.SetDir(tryMoveDir);
            }
            else
            {
                // _unitAnim.Play("idle");
            }
        }

        public void GetDamage(int damage)
        {
            if (damage > 0)
            {
                _unitViewController.ShowHitEffect();
                int damageAfterShp = damage - _resource.shp;
                if (damageAfterShp > 0)
                {
                    if (damage >= _resource.hp)
                    {
                        SetResource(new ChaRes(0, _resource.speed, 0));
                        Kill();
                        OnDie?.Invoke(this);
                        return;
                    }
                }

                if (damageAfterShp <= 0)
                {
                    SetResource(ChaResType.Shield, _resource.shp - damage);
                    return;
                }

                SetResource(ChaResType.Shield, 0);
                SetResource(ChaResType.Health, Mathf.Min(_resource.hp - damageAfterShp, _prop.hp));
            }

            if (damage < 0)
            {
                int heal = -damage;
                int healAfterAddHp = heal - (_prop.hp - _resource.hp);
                if (healAfterAddHp > 0)
                {
                    SetResource(ChaResType.Health, _prop.hp);
                    SetResource(ChaResType.Shield, healAfterAddHp);
                }
                else
                {
                    SetResource(ChaResType.Health, _resource.hp + heal);
                }
            }
        }

        public void Kill()
        {
            IsDead = true;
            // _unitAnim.Play("die");
            // gameObject.AddComponent<UnitRemover>().SetDuration(2f);
            GameObject dieEffect =
                Instantiate(ResLoader.LoadAsset<GameObject>($"Prefabs/Effects/{_unitViewController.View.name}_Die"));
            dieEffect.transform.position = transform.position;
            dieEffect.GetComponent<Animator>().Play("die");
            dieEffect.GetComponent<SpriteRenderer>().flipX = side != 0;
            // if (dieEffect == null)
            //     return;
            //
            dieEffect.AddComponent<UnitRemover>().SetDuration(dieEffect.GetComponent<SightEffect>().duration);
        }

        // public void OrderMove(Vector3 moveOrder)
        // {
        //     _moveOrder.x = moveOrder.x;
        //     _moveOrder.y = moveOrder.y;
        // }

        public void SetFace(Vector3 dir)
        {
            _unitAnim.SetDir(dir);
        }

        public void Play(string animName, float speed = 1.0f)
        {
            _unitAnim.Play(animName, speed);
        }
    }
}