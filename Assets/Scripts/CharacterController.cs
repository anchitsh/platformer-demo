using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float jumpForce;
    public int morejumps;
    private Rigidbody2D rb;
    public bool jump;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    public int defaultAdditionalJumps = 1;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private int bufferCounter = 0, coyBuffer = 0;
    public int bufferMax = 10, coyMax = 10;
    bool bufferBool;
    bool midjump;
    float gravityScaleAtStart;

    //Animator animator;
    public int midAir;
    bool walltouch;

    public static bool move;
    private Transform m_currMovingPlatform;


    public float walk_speed;
    public float y_max;

    public float horizontalAxis;

    public ParticleSystem jumpParticle;
    private bool groundTouch;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    private Vector3 velocityRef = Vector3.zero;

    public bool onRightWall;
    public bool onLeftWall;
    public float collisionRadius = 0.25f;
    public Vector2  rightOffset, leftOffset;

    Animator ani;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        morejumps = defaultAdditionalJumps;
        gravityScaleAtStart = rb.gravityScale;
        jump = false;
        midAir = 0;
        groundTouch = false;
        ani = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        Jump();
        BetterJump();
        BufferJump();
        CoyeteTime();
        if (isGrounded = true && rb.velocity.y == 0)
        {
            midjump = false;
        }
        CheckIfGrounded();
        CheckWalls();


        if (rb.velocity.y < y_max)
        {
            rb.velocity = new Vector2(rb.velocity.x, y_max);
        }
        mid_air();
        if (isGrounded && !groundTouch)
        {
            jumpParticle.Play();
            ani.SetTrigger("land");
            groundTouch = true;
        }
        if (!isGrounded && groundTouch)
        {
            groundTouch = false;
        }
        if (horizontalAxis>0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horizontalAxis<0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }


    }

    void Move()
    {

        horizontalAxis = Input.GetAxisRaw("Horizontal");
        float speed;
        if (isGrounded)
        {
            speed = walk_speed;
        }
        else
        {
            speed = walk_speed - .5f;
        }
        Vector3 targetVelocity = new Vector2(horizontalAxis * speed , rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocityRef, movementSmoothing);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && (isGrounded) && walltouch == false)
        {
            midjump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jump = true;
            transform.parent = null;
            jumpParticle.Play();
            ani.SetTrigger("jump");
        }
    }



    void mid_air()
    {
        if (jump == true && rb.velocity.y > 0)
        {
            midAir = 1;
        }
        else if (jump == true && rb.velocity.y < 0)
        {
            midAir = -1;
            if (isGrounded)
            {
                jump = false;
            }
        }
        else if (jump == true && rb.velocity.y == 0)
        {
            jump = false;
        }
        else if (jump == false)
        {
            midAir = 0;
        }

    }

    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void BufferJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == false)
        {
            bufferBool = true;
        }
        if (bufferBool == true)
        {
            bufferCounter++;
            if (isGrounded && !jump)
            {

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jump = true;
                jumpParticle.Play();
            }
            if (bufferCounter > bufferMax)
            {
                bufferBool = false;
                bufferCounter = 0;
            }

        }

    }
    void CoyeteTime()
    {
        if (isGrounded == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && coyBuffer < coyMax && midjump == false)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jump = true;
            }
            coyBuffer++;

        }
        else
        {
            coyBuffer = 0;
        }
    }
    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (colliders != null)
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }
    }
    void CheckWalls()
    {
        Collider2D colliders = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        if (colliders != null)
        {
            onRightWall = true;
        }
        else
        {
           onRightWall = false;
        }
        Collider2D colliders2= Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        if (colliders2 != null)
        {
            onLeftWall = true;
        }
        else
        {
            onLeftWall = false;
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere(isGroundedChecker.position, checkGroundRadius);
    }

}
