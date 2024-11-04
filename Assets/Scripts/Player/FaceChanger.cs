using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FaceChanger;

public class FaceChanger : MonoBehaviour
{
    [SerializeField] private Sprite defaultFace;
    [SerializeField] private Sprite damagedFace;
    [SerializeField] private Sprite jumpFace;
    [SerializeField] private Sprite fallingFace;
    [SerializeField] private Sprite deathFace;

    private SpriteRenderer faceRenderer;

    private Faces curentFace;
    private Sequence timeSequence;

    public enum Faces
    {
        Default,
        Damaged,
        Jump,
        Falling,
        Death
    }

    private void Start()
    {
        faceRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeFace(Faces face = Faces.Default)
    {
        Change(face);
        curentFace = face;
    }

    private void Change(Faces face)
    {
        switch (face)
        {
            case Faces.Default: faceRenderer.sprite = defaultFace; break;
            case Faces.Damaged: faceRenderer.sprite = damagedFace; break;
            case Faces.Jump: faceRenderer.sprite = jumpFace; break;
            case Faces.Falling: faceRenderer.sprite = fallingFace; break;
            case Faces.Death: faceRenderer.sprite = deathFace; break;
            default: faceRenderer.sprite = defaultFace; break;
        }
    }

    public void ChangeFaceForTime(float time, Faces face = Faces.Default)
    {
        if (timeSequence != null) timeSequence.Kill();

        timeSequence = DOTween.Sequence()
            .AppendCallback(() => Change(face))
            .AppendInterval(time)
            .AppendCallback(() => Change(curentFace))
            .SetLink(gameObject);
    }
}
