using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData : StorageData<SettingsData>
{
    public float masterVolume = 0.25f;
    public float musicVolume = 0.25f;
    public float SFXVolume = 0.25f;

    public void UpdateAudioVolume()
    {
        AudioManager.Instance.SetVolume("Master", masterVolume);
        AudioManager.Instance.SetVolume("Music", musicVolume);
        AudioManager.Instance.SetVolume("SFX", SFXVolume);
    }
}