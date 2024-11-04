using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerVolume : MonoBehaviour
{
    [SerializeField] private string name;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerComponent target)) return;
        AudioManager.Instance.Play(name);
    }
}
