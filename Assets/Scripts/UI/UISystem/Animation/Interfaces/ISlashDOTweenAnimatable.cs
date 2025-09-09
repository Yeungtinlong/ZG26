using UnityEngine;

namespace SupportUtils
{
    public interface ISlashDOTweenAnimatable
    {
        public RectTransform Content { get; }
    }

    public static class SlashDOTweenAnimatableExtensions
    {
        public static void Show(this ISlashDOTweenAnimatable animatable) { }

        public static void Hide(this ISlashDOTweenAnimatable animatable) { }

        public static void SetOpen(this ISlashDOTweenAnimatable animatable) { }

        public static void SetClose(this ISlashDOTweenAnimatable animatable) { }
    }
}