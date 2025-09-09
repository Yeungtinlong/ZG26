namespace TheGame
{
    [XLua.LuaCallCSharp]
    [XLua.CSharpCallLua]
    public enum ItemType
    {
        Currency = 0,
        Equipment = 1,
        Consumable = 2,
        Shard = 3,
        GameAsset = 4,
        Material = 5,
        Trap = 6,
    }
    
    [XLua.CSharpCallLua]
    public interface LItemConfig
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public string Description { get; set; }
    }
}