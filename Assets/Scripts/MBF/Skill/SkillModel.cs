namespace MBF
{
    [XLua.CSharpCallLua]
    public struct SkillModel
    {
        public string id;
        public string[] tags;
        public int cooldown;
        public TimelineModel effect;

        public SkillModel(string id, string[] tags, int cooldown, TimelineModel effect)
        {
            this.id = id;
            this.tags = tags;
            this.cooldown = cooldown;
            this.effect = effect;
        }
    }
}