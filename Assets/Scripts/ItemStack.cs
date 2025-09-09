namespace TheGame
{
    [XLua.LuaCallCSharp]
    public struct ItemStack
    {
        public string id;
        public int count;

        public ItemStack(string id, int count)
        {
            this.id = id;
            this.count = count;
        }

        public static ItemStack operator *(ItemStack a, int times)
        {
            return new ItemStack(a.id, a.count * times);
        }
    }
}