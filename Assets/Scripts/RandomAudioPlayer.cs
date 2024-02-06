using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomSound();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomSound();
        }
    }

    void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, clips.Length);
        audioSource.clip = clips[randomIndex];
        audioSource.Play();
    }
}