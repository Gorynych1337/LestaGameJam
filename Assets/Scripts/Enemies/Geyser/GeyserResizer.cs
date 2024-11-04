using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeyserResizer : MonoBehaviour
{
    [SerializeField] Transform gayserTransform;

    [SerializeField] private float upTime;
    [SerializeField] private float inUpPositionTime;
    [SerializeField] private float inDownPositionTime;

    [SerializeField] private List<Sprite> sprites;
    private IEnumerator<Sprite> spriteEnumerator;

    private void Start()
    {
        gayserTransform.parent.DOLocalMoveY(-gayserTransform.localScale.y / 2, 0);
        spriteEnumerator = GetSpriteEnumerator();
    }

    public void StartLoop()
    {
        var s = DOTween.Sequence();

        for (int i = 0; i < sprites.Count; i++)
        {
            s.AppendCallback(SetSprite)
                .Append(gayserTransform.parent.DOLocalMoveY(i * gayserTransform.localScale.y / 2 / (sprites.Count - 1), upTime / sprites.Count).SetEase(Ease.Linear));
        }

        s.AppendInterval(inUpPositionTime);

        for (int i = 0; i < sprites.Count; i++)
        {
            s.AppendCallback(SetSprite)
                .Append(gayserTransform.parent.DOLocalMoveY(i * -gayserTransform.localScale.y / 2 / (sprites.Count - 1), upTime / sprites.Count).SetEase(Ease.Linear));
        }

        s.AppendInterval(inDownPositionTime)
            .AppendCallback(ResetEnumerator)
            .SetLoops(-1);
    }

    private void ResetEnumerator()
    {
        spriteEnumerator = GetSpriteEnumerator();
    }

    private void SetSprite()
    {
        spriteEnumerator.MoveNext();
        var sprite = spriteEnumerator.Current;
        gayserTransform.parent.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }

    private IEnumerator<Sprite> GetSpriteEnumerator()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            yield return sprites[i];
        }

        for (int i = 0; i < sprites.Count; i++)
        {
            yield return sprites[sprites.Count - i - 1];
        }
    }
}
