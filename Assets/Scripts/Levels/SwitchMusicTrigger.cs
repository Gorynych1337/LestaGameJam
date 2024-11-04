using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicTrigger : MonoBehaviour
{
    [SerializeField] private string musicName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerComponent target)) AudioManager.Instance.Play(musicName, AudioType.Music, true);
    }
}
