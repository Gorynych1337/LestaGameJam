using System;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Sound,
    Music,
    Ambience
}

[CreateAssetMenu(fileName = "SoundCollection", menuName = "Sound Collection", order = 1)]
public class SoundCollection : ScriptableObject
{
    [Serializable]
    public struct Sound
    {
        public string name;
        public AudioType type;
        public AudioClip clip;
        public bool loop;
    }

    public List<Sound> sounds;
}