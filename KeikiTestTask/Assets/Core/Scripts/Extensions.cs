using UnityEngine;
using System.Collections;
using Spine.Unity;

public static class Extensions 
{
    public static Spine.TrackEntry QueueAnimation(this SkeletonAnimation skeletonAnimation,
                                       AnimationReferenceAsset animationReference, bool loop = false, float delay = 0)
    {
        return skeletonAnimation.AnimationState.AddAnimation(0, animationReference, loop, delay);
    }
    public static Spine.TrackEntry PlayAnimation(this SkeletonAnimation skeletonAnimation,
                                      AnimationReferenceAsset animationReference, bool loop = false)
    {
       return  skeletonAnimation.AnimationState.SetAnimation(0, animationReference, loop);
    }

    public static void SetCallbackAction(this Spine.TrackEntry trackEntry, System.Action action)
    {
        trackEntry.Complete += OnTrackEntryComplete;
        void OnTrackEntryComplete(Spine.TrackEntry entry)
        {
            action.Invoke();
            entry.Complete -= OnTrackEntryComplete;
        }
    }


    public static void SetStartAction(this Spine.TrackEntry trackEntry, System.Action action)
    {
        trackEntry.Start += OnTrackEntryStart;
        void OnTrackEntryStart(Spine.TrackEntry entry)
        {
            action.Invoke();
            entry.Complete -= OnTrackEntryStart;
        }
    }

}
