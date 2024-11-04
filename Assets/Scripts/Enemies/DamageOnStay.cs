using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnStay : MonoBehaviour
{
    [SerializeField] private float _damagePerTick;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerComponent target)) target.TakeDamage(_damagePerTick);
    }
}
