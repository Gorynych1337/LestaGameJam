using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{
    private const float splineOffset = 0.5f;
    [SerializeField] float modifier;
    [SerializeField]
    public SpriteShapeController spriteShape;
    [SerializeField]
    public Transform[] points;


    private void Awake()
    {
        UpdateVerticies();
    }

    private void Update()
    {
        UpdateVerticies();
    }

    private void UpdateVerticies()
    {
        if (points.Length < 2) return;
        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 vertex = points[i].localPosition;
            Vector2 towardsCenter = (Vector2.zero - vertex).normalized;
            float colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try
            {
                spriteShape.spline.SetPosition(i, (vertex - towardsCenter * colliderRadius * modifier));
            }
            catch
            {
                Debug.Log("Spline points are too close to each other… recalculate");
                spriteShape.spline.SetPosition(i, (vertex - towardsCenter * (colliderRadius + splineOffset)));
            }

            Vector2 _lt = spriteShape.spline.GetLeftTangent(i);

            Vector2 _newRt = Vector2.Perpendicular(towardsCenter) * _lt.magnitude;
            Vector2 _newLt = Vector2.zero - (_newRt);

            spriteShape.spline.SetRightTangent(i, _newRt);
            spriteShape.spline.SetLeftTangent(i, _newLt);
        }
    }
}
