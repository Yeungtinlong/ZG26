using System;

namespace TheGame.UI
{
    public interface IClickableContainer<out T>
    {
        public event Action<T> OnClick;
    }

    public interface IEventContainer<TEventArgs>
    {
        public event EventHandler<TEventArgs> OnEventRaised;
    }
}