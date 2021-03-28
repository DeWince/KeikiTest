using UnityEngine;
using System.Collections;

public class ScaleTweener : MonoBehaviour
{
    [SerializeField] private float blinkMaxScale = 1.5f;
    [SerializeField] private float blinkDuration = 1.5f;
    [SerializeField] private AnimationCurve blinkCurve = new AnimationCurve();
    IEnumerator blinkRoutine;
    public void StartScalingPingPong()
    {
        blinkRoutine = transform.ScaleTween(Vector3.one, Vector3.one * blinkMaxScale, blinkDuration, blinkCurve, true);
        StartCoroutine(blinkRoutine);
    }
    // Update is called once per frame
    public void StopScaling()
    {
        if (blinkRoutine !=null)
        {
            StopCoroutine(blinkRoutine);
        }
        StartCoroutine(transform.ScaleTween(transform.localScale, Vector3.one, 0.2f));
    }
    public void StopScalingImediate()
    {
        if (blinkRoutine != null)
        {
            StopCoroutine(blinkRoutine);
        }
        transform.localScale = Vector3.one;
    }
    private void OnDisable()
    {
        transform.localScale = Vector3.one;
        if (blinkRoutine != null)
        {
            StopCoroutine(blinkRoutine);
        }
    }
}
