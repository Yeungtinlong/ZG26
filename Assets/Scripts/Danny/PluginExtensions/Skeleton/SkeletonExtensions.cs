#if DANNY_SPINE_SUPPORT
using System;
using Spine;
using Spine.Unity;

namespace SupportUtils
{
    public static class SkeletonExtensions
    {
        public static SkeletonAnimation Play(this SkeletonAnimation skeletonAnimation, string animationName,
            bool loop = false, Action onComplete = null)
        {
            TrackEntry trackEntry = skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);
            trackEntry.Complete -= OnCompleted;
            trackEntry.Complete += OnCompleted;

            void OnCompleted(TrackEntry t)
            {
                trackEntry.Complete -= OnCompleted;
                onComplete?.Invoke();
            }

            return skeletonAnimation;
        }

        public static SkeletonAnimation PlayWithFallback(this SkeletonAnimation skeletonAnimation, string animationName,
            string fallbackName, bool loop = false, Action onComplete = null)
        {
            string realName = skeletonAnimation.Contains(animationName) ? animationName : fallbackName;
            return skeletonAnimation.Play(realName, loop, onComplete);
        }

        public static TrackEntry CurrentTrack(this SkeletonAnimation skeletonAnimation) =>
            skeletonAnimation.AnimationState.GetCurrent(0);

        public static SkeletonAnimation Append(this SkeletonAnimation skeletonAnimation, string animationName,
            bool loop)
        {
            // TrackEntry trackEntry = skeletonAnimation.AnimationState.GetCurrent(0);
            skeletonAnimation.AnimationState.AddAnimation(0, animationName, loop, 0f);
            
            // trackEntry.Complete += OnCompleted;
            //
            // void OnCompleted(TrackEntry t)
            // {
            //     trackEntry.Complete -= OnCompleted;
            //     skeletonAnimation.Play(animationName, loop);
            // }

            return skeletonAnimation;
        }

        public static bool Contains(this SkeletonAnimation skeletonAnimation, string animationName) =>
            skeletonAnimation.SkeletonDataAsset.GetSkeletonData(false).FindAnimation(animationName) != null;
    }
}
#endif