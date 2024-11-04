using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByVelocity : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var moveDirection = rb.velocity.normalized;
        Vector3 lookingDirection = transform.TransformDirection(Vector3.down);
        float angleDiff = Vector3.SignedAngle(lookingDirection, moveDirection, Vector3.forward);
        transform.Rotate(Vector3.forward, angleDiff);
    }
}
