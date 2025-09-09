namespace TheGame.GM
{
    [XLua.LuaCallCSharp]
    public enum CharacterType
    {
        None = 0,
        
        Tank,
        Warrior,
        Carry,
        Support,
        Assassin,
    }
}