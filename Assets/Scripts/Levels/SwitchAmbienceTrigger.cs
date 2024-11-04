using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAmbienceTrigger : MonoBehaviour
{
    [SerializeField] private string ambienceName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerComponent target)) AudioManager.Instance.Play(ambienceName, AudioType.Ambience, true);
    }
}
