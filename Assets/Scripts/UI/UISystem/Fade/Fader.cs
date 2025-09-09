namespace TheGame.UI
{
    public static class Fader
    {
        private static IFader _instance;

        public static IFader Instance => _instance ??= new OverlayFader(UIManager.Instance);
    }
}