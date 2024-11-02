using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Tweener tween;
    void Start()
    {
        var vec = transform.position + offset;
        tween = transform.DOMove(vec, 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }
}
