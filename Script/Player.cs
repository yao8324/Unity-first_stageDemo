using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("移动参数")]
    [SerializeField] public float moveSpeed = 12f;

    [Header("冲刺参数")]
    [SerializeField] public float dashSpeed = 15f;
    [SerializeField] public float dashDuration = 0.5f;
    [SerializeField] public float dashCoolDown;
    public float dashUsageTimer;
    public float dashDir {  get; private set; }


    [Header("跳跃参数")]
    [SerializeField] public float jumpForce = 16f;

    [Header("检测参数")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundCheckDistance;
    [Space]
    [SerializeField] public Transform wallCheck;
    [SerializeField] public float wallCheckDistance;
    [SerializeField] public LayerMask whatIsGround;


    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;




    #region 组件
    public Animator anim { get; set; }

    public Rigidbody2D rb { get; private set; }

    #endregion

    #region 状态机和状态
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }
    #endregion



    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        
        CheckForDashInput();
    }

    private void CheckForDashInput()
    {
        dashUsageTimer -= Time.deltaTime;



        if(Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCoolDown;

            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
            {
                dashDir = facingDir;
            }

            stateMachine.ChangeState(dashState);
        }
    }

    public void SetVelocity(float _xvelocity, float _yvelocity)
    {
        rb.linearVelocity = new Vector2(_xvelocity, _yvelocity);
        FlipController(_xvelocity);
    }

    public bool isGroundDetected() => Physics2D.Raycast( groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
}
