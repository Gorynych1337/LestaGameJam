using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private float _healSize;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Player target)) return;
        target.GetHealed(_healSize);
        Destroy(gameObject);
    }
}
