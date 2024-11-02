using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement: MonoBehaviour
{
    [SerializeField] private float movementForce;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb;
    private Inputs input;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new Inputs();
        input.Enable();

        input.PlayerMovement.Jump.performed += (ctx) => Jump();
    }

    private void Jump()
    {
        GetComponent<SoftBody>().SetSolid();
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        rb.AddForce(Vector2.right * movementForce * input.PlayerMovement.MovementAxis.ReadValue<float>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Ground") return;
        GetComponent<SoftBody>().SetLiquid();
    }
}
