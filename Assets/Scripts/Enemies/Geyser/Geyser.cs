using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] private float upForce; 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Rigidbody2D rb)) return;
        rb.AddForce(Vector2.up * upForce);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Rigidbody2D rb)) return;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/2);
    }
}
