using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFlight : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyTimer;

    private float startTime;
    private Vector3 _direction;

    public void Instantiate(Vector2 direction)
    {
        _direction = direction;
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        transform.position += _direction * speed * Time.fixedDeltaTime;
        if(Time.time - destroyTimer >= startTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Player target)) return;
        Destroy(gameObject);
    }
}
