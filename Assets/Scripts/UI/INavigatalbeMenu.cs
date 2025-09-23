namespace TheGame.UI
{
    public enum NavigationMenuType
    {
        Level = 0,
        Role,
        Daily,
        Shop,
        Strategy,
        Mission,
    }
    
    public interface INavigationMenu
    {
        NavigationMenuType Type { get; }
        void Set();
    }
}