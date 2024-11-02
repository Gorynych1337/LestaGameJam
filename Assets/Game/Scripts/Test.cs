using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Tweener tween;
    
    // Start is called before the first frame update
    void Start()
    {
        tween = transform.DOPunchPosition(Vector3.up * 60, 2f, 4)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }
}
