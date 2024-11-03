using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class CloudPlatformComponent : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float _loopTime;
    [SerializeField] private float _damage;

    [SerializeField] private Transform _startTransform;
    [SerializeField] private Transform _endTransform;


    private bool isRuntime;

    private void OnValidate()
    {
        if (_startTransform is null)
        {
            _startTransform = new SelectionSphere().GetSphere("Start point").transform;
            _startTransform.position = transform.position;
            _startTransform.SetParent(transform);
        }
        if (_endTransform is null)
        {
            _endTransform = new SelectionSphere().GetSphere("End point").transform;
            _endTransform.position = transform.position;
            _endTransform.SetParent(transform);
        }


    }

    private void OnDrawGizmos()
    {
        if (isRuntime) return;
        Gizmos.color = Color.red;

        Gizmos.DrawLine(_startTransform.position, _endTransform.position);

        Gizmos.color = Color.green;

        Gizmos.DrawSphere(_startTransform.position, 0.5f);
        Gizmos.DrawSphere(_endTransform.position, 0.5f);


    }

    void Start()
    {
        isRuntime = true;
        Vector2 targetPose = _endTransform.position;
        this.transform.position = _startTransform.position;
        transform.DOMove(targetPose, _loopTime)
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
