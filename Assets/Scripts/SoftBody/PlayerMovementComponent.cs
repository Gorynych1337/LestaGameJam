using UnityEngine;

[RequireComponent(typeof(SoftBodyComponent), typeof(Rigidbody2D))]
public class PlayerMovementComponent: MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SoftBodyComponent softBodyComponent;
    [SerializeField] private Rigidbody2D rb;
    [Header("Settings")]
    [SerializeField] private float movementForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown = 2f;
    
    private float _groundedTime;
    private bool _isGrounded;
    private float _lastJumpTime;
    private Inputs _input;
    private float _size = 0f;
    private bool CanJump => Time.time - _groundedTime > jumpCooldown && _isGrounded;
    
    private void Start()
    {
        _input = new Inputs();
        _input.Enable();
        _input.PlayerMovement.Jump.performed += (ctx) => Jump();
    }

    private void Jump()
    {
        if (!CanJump) return;
        _isGrounded = false;
        _lastJumpTime = Time.time;
        softBodyComponent.SetSolid();
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _size += 0.001f;
            softBodyComponent.SetDistance(_size);
        }
        
        var value = _input.PlayerMovement.MovementAxis.ReadValue<float>();
        if (value == 0) return;
        rb.velocity = new Vector2(movementForce * value, rb.velocity.y);
    }

    private void Update()
    {
        if (!(Time.time - _lastJumpTime > 0.5f)) return;
        softBodyComponent.SetLiquid();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Ground")) return;
        _isGrounded = true;
        _groundedTime = Time.time;
        softBodyComponent.SetLiquid();
    }
}
