using System.Collections.Generic;
using MBF;

namespace TheGame.GM
{
    /// <summary>
    /// NOTE: 黑板，存放Scene中被游戏系统关注的一切，逻辑上的Scene, 思考：黑板是否应该属于GameManager?
    /// 子弹依赖角色，因为子弹只能打中角色，
    /// Aoe依赖角色、子弹，因为Aoe可以作用于子弹或角色，
    /// 所以提供一个黑板，让系统能访问到关注的数据
    /// </summary>
    public sealed class SceneVariants
    {
        public Map map;
        
        public readonly List<CharacterState> characters = new List<CharacterState>();
        public readonly List<BulletState> bullets = new List<BulletState>();
        public readonly List<AoeState> aoes = new List<AoeState>();

        public void Clear()
        {
            characters.Clear();
            bullets.Clear();
            aoes.Clear();
        }
    }
}