using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnStay : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool perTick;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerComponent target)) return;
        target.TakeDamage(damage, perTick);
    }
}
