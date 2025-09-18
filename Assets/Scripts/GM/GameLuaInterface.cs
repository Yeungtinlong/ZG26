using System.Collections.Generic;
using System.Linq;
using MBF;
using SupportUtils;
using UnityEngine;
using TheGame.Common;
using TheGame.InputSystem;
using TheGame.ResourceManagement;
using TheGame.UI;

namespace TheGame.GM
{
    [XLua.CSharpCallLua]
    public delegate float MoveSpeedDelegate(int moveSpeed);

    [XLua.CSharpCallLua]
    public delegate float SkillSpeedDelegate(int skillSpeed);

    [XLua.CSharpCallLua]
    public delegate int DamageDelegate(DamageInfo damageInfo);

    [XLua.CSharpCallLua]
    public delegate float RangeDelegate(int range);

    /// <summary>
    /// TODO: 可以从Lua中读取，暂时偷个懒
    /// </summary>
    public static class DesignerFormula
    {
        private static MoveSpeedDelegate _moveSpeedDelegate;
        private static SkillSpeedDelegate _skillSpeedDelegate;
        private static DamageDelegate _damageDelegate;
        private static RangeDelegate _rangeDelegate;

        public static void Init()
        {
            _moveSpeedDelegate = LuaManager.LuaEnv.Global.GetInPath<MoveSpeedDelegate>("Game.Designer.Formula.GetMoveSpeed");
            _skillSpeedDelegate = LuaManager.LuaEnv.Global.GetInPath<SkillSpeedDelegate>("Game.Designer.Formula.GetSkillSpeed");
            _damageDelegate = LuaManager.LuaEnv.Global.GetInPath<DamageDelegate>("Game.Designer.Formula.GetDamage");
            _rangeDelegate = LuaManager.LuaEnv.Global.GetInPath<RangeDelegate>("Game.Designer.Formula.GetRange");
        }
        
        public static int GetGradeProp(int baseValue, int plus, int times, int grade)
        {
            return (baseValue + (grade - 1) * plus) * (1 + (grade - 1) * times);
        }

        public static ChaProp GetGradeProp(ChaProp baseValue, ChaProp plus, ChaProp times, int grade)
        {
            return (baseValue + plus * (grade - 1)) * (times * (grade - 1));
        }

        public static float GetMoveSpeed(int moveSpeed)
        {
            return _moveSpeedDelegate(moveSpeed);
        }

        public static float GetSkillSpeed(int skillSpeed)
        {
            return _skillSpeedDelegate(skillSpeed);
        }

        public static int GetDamage(DamageInfo damageInfo)
        {
            return _damageDelegate(damageInfo);
        }

        public static float GetRange(int range)
        {
            return _rangeDelegate(range);
        }
    }

    [XLua.LuaCallCSharp]
    public static class GameLuaInterface
    {
        public static GameManager game;
        public static InputManager input;

        public static void CreateBullet(BulletLauncher bulletLauncher)
        {
            game.Bullet.CreateBullet(bulletLauncher);
        }

        public static void CreateAoe(AoeLauncher aoeLauncher)
        {
            game.Aoe.CreateAoe(aoeLauncher);
        }

        public static void RemoveAoe(AoeState aoeState, bool immediate = false)
        {
            game.Aoe.RemoveAoe(aoeState, immediate);
        }

        public static void CreateTimeline(TimelineObj timelineObj)
        {
            game.Timeline.CreateTimeline(timelineObj);
        }

        public static void CreateDamage(GameObject attacker, GameObject defender, Damage damage, DamageInfoTag[] tags)
        {
            game.Damage.CreateDamage(attacker, defender, damage, tags);
        }

        public static SightEffect CreateSightEffect(string effectName, Vector3 position)
        {
            SightEffect sightEffect = Object.Instantiate(ResLoader.LoadAsset<GameObject>($"{PathHelper.GetPrefabPath($"Effects/{effectName}")}"), position, Quaternion.identity).GetComponent<SightEffect>();
            sightEffect.gameObject.AddComponent<UnitRemover>().SetDuration(sightEffect.duration);
            return sightEffect;
        }

        public static void PopText(string text, float scale, Color color, Vector3 position)
        {
            CreateSightEffect("PopMessageText", position).GetComponent<PopMessageText>().Set(text, scale, color);
        }

        public static GameObject GetNearestTarget(GameObject finder, int side, bool includeFoe, bool includeAlly, float radius)
        {
            List<CharacterState> characters = game.SceneVariants.characters;
            float minSqrDistance = float.MaxValue;
            GameObject nearestTarget = null;
            float sqrRadius = radius * radius;
            for (int i = 0; i < characters.Count; i++)
            {
                CharacterState cs = characters[i];
                if ((cs.side == side && !includeAlly) || (cs.side != side && !includeFoe))
                    continue;
                float sqrDis = (finder.transform.position - cs.transform.position).sqrMagnitude;
                if (sqrDis >= minSqrDistance || sqrDis >= sqrRadius)
                    continue;

                minSqrDistance = sqrDis;
                nearestTarget = cs.gameObject;
            }

            return nearestTarget;
        }

        public static CharacterState CreateCharacter(string id, int side, int grade)
        {
            return game.CreateCharacter(id, side, grade);
        }

        public static void GetItem(string itemId, int count)
        {
            GameRuntimeData.Instance.GetItem(itemId, count);
        }

        public static bool RemoveItem(string itemId, int count)
        {
            return GameRuntimeData.Instance.RemoveItem(itemId, count);
        }

        public static void UnlockCharacter(string chaId)
        {
            GameRuntimeData.Instance.GetCharacter(chaId);
        }

        public static List<CharacterState> GetAllTargets(GameObject finder, int side, bool includeFoe, bool includeAlly, float radius)
        {
            return GetTargetsInRange(finder.transform.position, side, includeFoe, includeAlly, radius);
        }

        public static List<CharacterState> GetTargetsInRange(Vector3 center, int side, bool includeFoe, bool includeAlly, float radius)
        {
            List<CharacterState> characters = game.SceneVariants.characters;
            List<CharacterState> targets = new List<CharacterState>();
            float sqrRadius = radius * radius;
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].IsDead) continue;

                if ((characters[i].side == side && includeAlly) || (characters[i].side != side && includeFoe))
                {
                    if ((characters[i].transform.position - center).sqrMagnitude < sqrRadius)
                    {
                        targets.Add(characters[i]);
                    }
                }
            }

            return targets;
        }

        public static Vector3 GetMouseWorldPosition()
        {
            return (Vector2)game.Camera.MainCamera.ScreenToWorldPoint(input.InputState.MousePosition);
        }

        public static CharacterState RandomGetCharacterOfType(CharacterType characterType, int side, bool includeFoe, bool includeAlly)
        {
            return game.SceneVariants.characters.Where(c =>
                (c != null && !c.IsDead)
                &&
                ((c.side != side && includeFoe) || (c.side == side && includeAlly))
                &&
                c.Grid.Type == characterType
            ).RandomPickOne();
        }
        
        public static CharacterState RandomGetCharacter(int side, bool includeFoe, bool includeAlly)
        {
            return game.SceneVariants.characters.Where(c =>
                (c != null && !c.IsDead)
                &&
                ((c.side != side && includeFoe) || (c.side == side && includeAlly))
            ).RandomPickOne();
        }

        public static List<CharacterState> RandomGetCharacters(int side, bool includeFoe, bool includeAlly, int maxCount)
        {
            return game.SceneVariants.characters.Where(c =>
                (c != null && !c.IsDead)
                &&
                ((c.side != side && includeFoe) || (c.side == side && includeAlly))
            ).ToList().RandomPick(maxCount);
        }

        public static List<CharacterState> RandomGetCharactersOfType(CharacterType characterType, int count, int side, bool includeFoe, bool includeAlly)
        {
            return game.SceneVariants.characters.Where(c =>
                (c != null && !c.IsDead)
                &&
                ((c.side != side && includeFoe) || (c.side == side && includeAlly))
                &&
                c.Grid.Type == characterType
            ).ToList().RandomPick(count);
        }

        public static CharacterState MeleeFindSingleFoe(CharacterState caster)
        {
            CharacterState target = null;
            var enemies = game.SceneVariants.characters.Where(c => c != null && !c.IsDead && c.side != caster.side).ToList();
            // 选出坦克
            var tanks = enemies.Where(e => e.Grid.Type == CharacterType.Tank).ToList();
            if (tanks.Count > 0)
                target = tanks.RandomPickOne();
            // 没有坦克则随机选出
            if (target == null)
                target = enemies.RandomPickOne();
            return target;
        }

        public static List<CharacterState> GetAllCharacters()
        {
            return game.SceneVariants.characters;
        }
    }
}