using System;

namespace SupportUtils
{
    public interface IDOTweenAnimatable
    {
        public void Show(Action onComplete = null);
        public void Hide(Action onComplete = null);
        public void SetOpen();
        public void SetClose();
    }
}