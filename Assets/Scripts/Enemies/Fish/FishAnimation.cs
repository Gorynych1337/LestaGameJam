using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SplashCreator))]
public class FishAnimation : MonoBehaviour
{
    [SerializeField] private Transform _upPoint;
    [SerializeField] private float cooldown;
    [SerializeField] private float flightDuration;
    [SerializeField] private float rotateDuration;
    [SerializeField] private Transform fishTransform;

    private bool isAnimation = false;

    private bool isRuntime;

    void Start()
    {
        isRuntime = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (isAnimation) return;
        isAnimation = true;

        Animation();
    }

    private void Animation()
    {
        DOTween.Sequence()
            .Append(fishTransform.DOLocalMoveY(_upPoint.localPosition.y, flightDuration).SetEase(Ease.OutQuart))
            .AppendCallback(Splash)
            .Append(fishTransform.DOLocalRotate(new Vector3(0, 0, 180), rotateDuration))
            .Append(fishTransform.DOLocalMoveY(0, flightDuration).SetEase(Ease.InQuart))
            .Append(fishTransform.DOLocalRotate(new Vector3(0, 0, 0), 0))
            .AppendInterval(cooldown)
            .AppendCallback(EndAnimation)
            .SetLink(gameObject);
    }

    private void Splash()
    {
        GetComponent<SplashCreator>().CreateSplash(fishTransform.position);
    }

    private void EndAnimation()
    {
        isAnimation = false;
    }

    private void OnValidate()
    {
        if (_upPoint is null)
        {
            _upPoint = new SelectionSphere().GetSphere("Up point").transform;
            _upPoint.position = transform.position;
            _upPoint.SetParent(transform);
        }
    }

    private void OnDrawGizmos()
    {
        if (isRuntime) return;
        Gizmos.color = Color.green;
        _upPoint.localPosition = new Vector3(0, _upPoint.localPosition.y, 0);
        Gizmos.DrawSphere(_upPoint.position, 0.5f);
    }
}
