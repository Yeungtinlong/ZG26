using MBF;

namespace TheGame
{
    [XLua.CSharpCallLua]
    public interface LProductConfig
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int Limits { get; set; }
        public string[] Tags { get; set; }
        
        public ItemStack Price { get; set; }
        public TimelineNode Effect { get; set; }
    }
}