using System;
using System.Collections.Generic;
using Game.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType
{
    Sound,
    Music
}

public class AudioManager : SingletonBehaviour<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private SoundCollection soundCollection;
    
    private readonly Dictionary<string, SoundCollection.Sound> _sounds = new();
    
    public AudioMixer AudioMixer => audioMixer;
    
    private void Awake()
    {
        foreach (var sound in soundCollection.sounds)
        {
            _sounds[sound.name] = sound;
        }
        
        DontDestroyOnLoad(this);
    }
    
    public void Play(string name, AudioType type, bool loop = false)
    {
        if (!_sounds.TryGetValue(name, out var sound)) return;
        switch (type)
        {
            case AudioType.Sound:
                soundSource.PlayOneShot(sound.clip);
                break;
            case AudioType.Music:
                musicSource.clip = sound.clip;
                musicSource.loop = loop;
                musicSource.Play();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    public void Stop(AudioType type)
    {
        switch (type)
        {
            case AudioType.Sound:
                soundSource.Stop();
                break;
            case AudioType.Music:
                musicSource.Stop();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    public void Pause(AudioType type)
    {
        switch (type)
        {
            case AudioType.Sound:
                soundSource.Pause();
                break;
            case AudioType.Music:
                musicSource.Pause();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    public void UnPause(AudioType type)
    {
        switch (type)
        {
            case AudioType.Sound:
                soundSource.UnPause();
                break;
            case AudioType.Music:
                musicSource.UnPause();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    /// <summary>
    /// Set the volume of the audio mixer group.
    /// </summary>
    /// <param name="name"> audio mixer group </param>
    /// <param name="volume"> Range [0.0f, 1.0f] </param>
    public void SetVolume(string name, float volume)
    {
        audioMixer.SetFloat(name, LogarithmicValue(volume));
    }
    
    private static float CorrectValue(float rawValue) => Mathf.Max(rawValue, 0.0001f);
    
    private static float LogarithmicValue(float rawValue) => Mathf.Log(CorrectValue(rawValue)) * 20f;
}