using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(SoftBodyComponent), typeof(Rigidbody2D))]
public class PlayerComponent: MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SoftBodyComponent softBodyComponent;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private FaceChanger faceChanger;
    
    [Header("Character Settings")]
    [SerializeField] private float health;
    
    [Header("Movement Settings")]
    [SerializeField] private float movementForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown = 2f;

    [Header("HP Settings")]
    [SerializeField] private float minHP;
    [SerializeField] private float maxHP;

    [SerializeField] private float invulnerableTime = 1f;

    private bool CanJump => Time.time - _groundedTime > jumpCooldown && _isGrounded;
    
    private bool _isDead;
    private bool _isInvulnerable;
    private float _groundedTime;
    private bool _isGrounded;
    private float _lastJumpTime;
    private Inputs _input;
    private float _scaleMod;
    private float _healthDiff;
    
    private float Health
    {
        get => health;
        set
        {
            if (value <= minHP)
            {
                health = minHP;
                Die();
            }
            else
            {
                _healthDiff =  health - value;
                health = value;
                health = Mathf.Min(health, maxHP);
                Resize();
            }
        }
    }

    public void TakeDamage(float damage, bool perTick = false)
    {
        if (!perTick)
        {
            if (_isInvulnerable) return;
            _isInvulnerable = true;
            DOVirtual.DelayedCall(invulnerableTime, () => _isInvulnerable = false);
            AudioManager.Instance.Play("Damage");
        }
        
        Health -= damage;
        faceChanger.ChangeFaceForTime(0.5f, FaceChanger.Faces.Damaged);
    }

    public void TakeHeal(float heal)
    {
        Health += heal;
        AudioManager.Instance.Play("Vsasivaet");
    }

    private void Resize()
    {
        transform.localScale = Vector3.one * health * _scaleMod / 100;
        softBodyComponent.ResizeSpringDistance(_healthDiff * (_scaleMod * 2) / 100);
    }

    public void Die()
    {
        if(_isDead) return;
        _isDead = true;
        faceChanger.ChangeFaceConstant(FaceChanger.Faces.Death);
        AudioManager.Instance.Play("Death");
        GameManager.Instance.FadeWithLoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void Respawn()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        _scaleMod = transform.localScale.x;
        _isDead = false;

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
        AudioManager.Instance.Play("jump");
        _lastJumpTime = Time.time;
        softBodyComponent.SetSolid();
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        faceChanger.ChangeFaceForTime(0.5f, FaceChanger.Faces.Jump);
    }

    private void FixedUpdate()
    {
        var value = _input.PlayerMovement.MovementAxis.ReadValue<float>();
        
        if (value == 0) return;
        rb.velocity = new Vector2(movementForce * value, rb.velocity.y);
    }

    private void Update()
    {
        if (!_isGrounded && rb.velocity.y < 0) faceChanger.ChangeFace(FaceChanger.Faces.Falling);

        if (transform.position.y < -100) Die();

        if (!(Time.time - _lastJumpTime > 0.5f)) return;
        softBodyComponent.SetLiquid();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Ground")) return;
        _isGrounded = true;
        faceChanger.ChangeFace();
        _groundedTime = Time.time;
        softBodyComponent.SetLiquid();
    }
    
    private void OnDestroy()
    {
        _input.Disable();
    }
}
