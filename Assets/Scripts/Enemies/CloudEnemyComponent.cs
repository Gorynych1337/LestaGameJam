using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemyComponent : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float _loopTime;
    [SerializeField] private float _damage;

    private Tweener tween;
    void Start()
    {
        var vec = transform.position + offset;
        tween = transform.DOMove(vec, _loopTime)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player target))
        {
            target.GetDamage(_damage);
        }
    }
}
