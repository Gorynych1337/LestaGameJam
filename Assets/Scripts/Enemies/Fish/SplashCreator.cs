using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashCreator : MonoBehaviour
{
    [SerializeField] private int dropCount;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private float splashForce;

    public void CreateSplash(Vector3 position)
    {
        for (int i = 0; i < dropCount; i++)
        {
            var drop = Instantiate(dropPrefab);
            drop.transform.position = position;
            float angle = i * 180 * Mathf.Deg2Rad / (dropCount - 1);
            drop.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * splashForce);
        }
    }
}
