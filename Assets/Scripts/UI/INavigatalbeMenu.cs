namespace TheGame.UI
{
    public enum NavigationMenuType
    {
        Role,
        Daily,
        Shop,
        Strategy,
    }


    public interface INavigationMenu
    {
        NavigationMenuType Type { get; }
        void Set();
    }
}