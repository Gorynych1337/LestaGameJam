using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData : StorageData<SettingsData>
{
    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float SFXVolume = 1f;

    public void UpdateAudioVolume()
    {
        AudioManager.Instance.SetVolume("Master", masterVolume);
        AudioManager.Instance.SetVolume("Music", musicVolume);
        AudioManager.Instance.SetVolume("SFX", SFXVolume);
    }
}