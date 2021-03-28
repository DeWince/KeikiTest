using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "RndSound", menuName = "SoundEvents/Random")]
public class RandomSound : SoundEvent
{
    private const float Cooldown = 2f;
    [SerializeField] private AudioClip[] clips = default;
    [Range(0, 1)]
    [SerializeField] private float volume = default;
    private int lastTrackIndex = -1;
    public override void Play(AudioSource audioSource)
    {
        if (audioSource.time<Cooldown && audioSource.isPlaying)
        {
            return;
        }
        int randomIndex = Random.Range(0, clips.Length);
        if (randomIndex == lastTrackIndex)
        {
            IncrementIndex(ref randomIndex);
        }
        lastTrackIndex = randomIndex;
        audioSource.clip = clips[randomIndex];
        audioSource.volume = volume;
        audioSource.Play();
        
    }
    void IncrementIndex(ref int index)
    {
        index++;
        if (index>=clips.Length)
        {
            index = 0;
        }
    }
}
