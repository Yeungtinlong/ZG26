using MBF;
using TheGame.GM;

namespace TheGame
{
    [XLua.CSharpCallLua]
    public interface LCharacterConfig
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Tags { get; set; }
        public CharacterType CharacterType { get; set; }
        public string Description { get; set; }
        public string Prefab { get; set; }
        public string[] SkillIds { get; set; }
        public ChaProp BaseProp { get; set; }
        /// <summary>
        /// 属性成长率，长度为2的数组
        /// [0]: 固定值
        /// [1]: 百分比
        /// </summary>
        public ChaProp[] PropGrowth { get; set; }
    }
}