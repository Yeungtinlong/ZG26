using Cysharp.Threading.Tasks;

namespace TheGame.UI
{
    public class OverlayFader : IFader
    {
        private UIManager _uiManager;

        private LoadingScreen _fader;

        private bool _isFadingOut = false;
        private bool _isFadingIn = false;

        public OverlayFader(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public async UniTask FadeOut()
        {
            if (_isFadingOut)
                return;
            
            _isFadingOut = true;
            if (_fader != null)
            {
                await _fader.FadeOut(0.5f);
            }
            else
            {
                // _fader = await _uiManager.OpenUI<LoadingScreen>().FadeOut(0.5f);
            }
            _isFadingOut = false;
        }
        
        public async UniTask FadeIn()
        {
            if (_isFadingIn)
                return;
            
            _isFadingIn = true;
            if (_fader != null)
            {
                await _fader.FadeIn(0.5f);
            }
            else
            {
                // _fader = await _uiManager.OpenUI<LoadingScreen>().FadeIn(0.5f);
            }
            
            _isFadingIn = false;

            // if (_fader != null)
            //     _uiManager.CloseUI(_fader);
        }
    }
}