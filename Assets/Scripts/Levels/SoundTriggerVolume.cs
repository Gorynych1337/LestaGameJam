using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerVolume : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private bool _isActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerComponent target)) return;

        if (_isActive)
            AudioManager.Instance.Play(name);
        else
            AudioManager.Instance.Stop(name);

    }
}
