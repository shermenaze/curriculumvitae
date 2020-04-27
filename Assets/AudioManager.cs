using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public static AudioManager Instance => _instance;
    
    private static AudioManager _instance;


    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else _instance = this;
        
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, float volume = 0.1f)
    {
        if (_audioSource) _audioSource.PlayOneShot(clip, volume);
    }
}
