using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class Tweens
{
    public static IEnumerator GenericTween<T>(
        T target, System.Action<T, float> InterpolationAction, float duration = 1, AnimationCurve curve = null,
        bool pingPong = false)
    {
        float time = 0;
        bool finished = false;
        float sign = 1;
        while (!finished)
        {
            time += sign * Time.deltaTime;
            if (pingPong)
            {
                if (time >= duration)
                {
                    time = duration;
                    sign = -1;
                }
                if (time <= 0)
                {
                    time = 0;
                    sign = 1;
                }
            }
            else
            {
                if (time >= duration)
                {
                    time = duration;
                    finished = true;
                }
            }
            float relativeTime = time / duration;
            float t;
            if (curve != null)
            {
                t = curve.Evaluate(relativeTime);
            }
            else
            {
                t = relativeTime;
            }
            InterpolationAction?.Invoke(target,t);
            yield return new WaitForFixedUpdate();
        }
    }
    public static IEnumerator ScaleTween(this Transform transform, Vector3 from, Vector3 to, float duration = 1, AnimationCurve curve =null, bool pingPong = false)
    {
        return GenericTween<Transform>(transform,TweenAction,duration,curve,pingPong);
        void TweenAction(Transform thisTransform,float t)
        {
            thisTransform.localScale = Vector3.Lerp(from, to, t);
        }

    }
    public static IEnumerator MoveTween(this Transform transform, Vector3 from, Vector3 to, float duration = 1, AnimationCurve curve = null, bool pingPong = false)
    {
        return GenericTween<Transform>(transform, TweenAction, duration, curve, pingPong);
        void TweenAction(Transform thisTransform, float t)
        {
            thisTransform.position = Vector3.Lerp(from, to, t);
        }

    }
    public static IEnumerator ColorTween(this SpriteRenderer renderer, Color from, Color to, float duration = 1, AnimationCurve curve = null, bool pingPong = false)
    {
        return GenericTween<SpriteRenderer>(renderer, TweenAction, duration, curve, pingPong);
        void TweenAction(SpriteRenderer thisRenderer, float t)
        {
            thisRenderer.color = Color.Lerp(from, to, t);
        }

    }
    public static IEnumerator ColorTween(this Image image, Color from, Color to, float duration = 1, AnimationCurve curve = null, bool pingPong = false)
    {
        return GenericTween<Image>(image, TweenAction, duration, curve, pingPong);
        void TweenAction(Image thisImage, float t)
        {
            thisImage.color = Color.Lerp(from, to, t);
        }

    }
    public static IEnumerator TweenCallback(this Coroutine tweenRoutine, System.Action callback)
    {
        yield return tweenRoutine;
        callback?.Invoke();
    }

   
}
