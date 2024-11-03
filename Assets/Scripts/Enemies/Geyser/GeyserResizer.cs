using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserResizer : MonoBehaviour
{
    [SerializeField] private float upTime;
    [SerializeField] private float inUpPositionTime;
    [SerializeField] private float inDownPositionTime;

    private void Start()
    {
        transform.parent.DOLocalMoveY(-transform.lossyScale.y / 2, 0);
    }

    public void StartLoop()
    {
        DOTween.Sequence()
            .Append(transform.parent.DOLocalMoveY(transform.lossyScale.y / 2, upTime).SetEase(Ease.OutQuad))
            .AppendInterval(inUpPositionTime)
            .Append(transform.parent.DOLocalMoveY(-transform.lossyScale.y / 2, upTime).SetEase(Ease.InQuad))
            .AppendInterval(inDownPositionTime)
            .SetLoops(-1);
    }
}
