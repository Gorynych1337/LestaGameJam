using UnityEngine;

[RequireComponent(typeof(SoftBodyComponent), typeof(Rigidbody2D))]
public class PlayerComponent: MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SoftBodyComponent softBodyComponent;
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Character Settings")]
    [SerializeField] private float health;
    
    [Header("Movement Settings")]
    [SerializeField] private float movementForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown = 2f;
    
    private float _groundedTime;
    private bool _isGrounded;
    private float _lastJumpTime;
    private Inputs _input;
    private bool CanJump => Time.time - _groundedTime > jumpCooldown && _isGrounded;

    public float Health
    {
        get => health;
        set
        {
            if (value <= 0)
            {
                health = 0;
                Die();
            }
            else
            {
                health = value;
            }
        }
    }

    private void Die()
    {
        throw new System.NotImplementedException();
    }
    
    private void Respawn()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        _input = new Inputs();
        _input.Enable();
        _input.PlayerMovement.Jump.performed += (ctx) => Jump();
        _input.PlayerControlls.Pause.performed += (ctx) =>
        {
            if (GameManager.Instance.IsPaused) GameManager.Instance.ResumeGame();
            else GameManager.Instance.PauseGame();
        };
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
    
    private void OnDestroy()
    {
        _input.Disable();
    }
}
