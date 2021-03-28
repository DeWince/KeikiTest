using UnityEngine;
using System.Collections;
using System;

public class Timer 
{
    private float timeLeft = 0;

    public Action onTimerReady;

    public Timer()
    {
    }
    public Timer(float timeLeft)
    {
        this.TimeLeft = timeLeft;
    }

    public bool Ready { get; private set; } = true;
    public float TimeLeft { 
        get => timeLeft;
        set 
        {

            timeLeft = value;
            if (timeLeft <= 0)
            {
                if (!Ready)
                {
                    Ready = true;
                    onTimerReady?.Invoke();
                }
                timeLeft = 0;
            }
            else
            {
                if (Ready)
                {
                    Ready = false;
                }
            }
        }
    }
}
