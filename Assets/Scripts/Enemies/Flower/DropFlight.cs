using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFlight : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 _direction;

    public void Instantiate(Vector2 direction)
    {
        _direction = direction;
    }

    private void FixedUpdate()
    {
        transform.position += _direction * speed * Time.fixedDeltaTime;
    }
}
