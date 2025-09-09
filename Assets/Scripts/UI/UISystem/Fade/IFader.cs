using Cysharp.Threading.Tasks;

namespace TheGame.UI
{
    public interface IFader
    {
        public UniTask FadeIn();
        public UniTask FadeOut();
    }
}