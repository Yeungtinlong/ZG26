namespace TheGame.UI
{
    public enum NavigationMenuType
    {
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