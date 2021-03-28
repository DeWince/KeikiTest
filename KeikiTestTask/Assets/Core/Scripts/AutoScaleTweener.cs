using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaleTweener : ScaleTweener
{
    // Start is called before the first frame update
    void OnEnable()
    {
        StartScalingPingPong();
    }


}
