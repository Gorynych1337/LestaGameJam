using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundCollection", menuName = "Sound Collection", order = 1)]
public class SoundCollection : ScriptableObject
{
    [Serializable]
    public struct Sound
    {
        public string name;
        public AudioClip clip;
    }

    public List<Sound> sounds;
}