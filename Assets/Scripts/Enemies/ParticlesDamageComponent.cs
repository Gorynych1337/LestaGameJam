using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDamageComponent : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _coolDown;
    private bool _isReady;

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out PlayerComponent target) && _isReady)
            TakeDamage(target);
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_coolDown);

        _isReady = true;
    }

    private void TakeDamage(PlayerComponent target)
    {
        _isReady = false;

        target.Health -= _damage;

        StartCoroutine(CoolDown());
    }

    private void Start()
    {
        _isReady = true;
    }
}
