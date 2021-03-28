using UnityEngine;
using System.Collections;

public abstract class SoundEvent : ScriptableObject
{
    public abstract void Play(AudioSource audioSource);
}