using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float destroyTimer;

    private float startTime;

    public void Awake()
    {
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (Time.time - destroyTimer >= startTime) Destroy(gameObject);
    }
}
