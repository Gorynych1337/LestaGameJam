using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeSoundVolume : MonoBehaviour
{
    [SerializeField] private List<string> _sounds;
    [SerializeField] private float _minRange = 3f;
    [SerializeField] private float _maxRange = 6f;
    private bool _isPlaying;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || _isPlaying)
            return;

        _isPlaying = true;
        
        DOVirtual.DelayedCall(Random.Range(_minRange, _maxRange), () =>
        {
            AudioManager.Instance.Play(_sounds[Random.Range(0, _sounds.Count)]);
            _isPlaying = false;
        });
    }
}
