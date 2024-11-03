using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDamageComponent : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out Player target))
            target.GetDamage(_damage);
    }
}
