using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;

    [HideInInspector]
    public float horizontalAxis;

    [Header("Movement Settings")]
    public float walkSpeed = 3;
    public float airSpeed = 2.5f;
    public float maxFallSpeed = -5;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    private Vector3 velocityRef = Vector3.zero;

    [Header("Jump Settings")]
    public float jumpForce;

    [HideInInspector]
    public bool jump;

    [Header("More Jump Settings")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private int bufferCounter = 0, coyBuffer = 0;
    public int bufferMax = 10, coyMax = 10;
    private bool bufferBool;
    private bool midjump;

    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool onRightWall;
    [HideInInspector]
    public bool onLeftWall;

    [Header("collider checks")]
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public float wallCollisionRadius = 0.15f;
    public LayerMask groundLayer;
    private Vector2 rightOffset = new Vector2(0.09f, 0);
    private Vector2 leftOffset = new Vector2(-0.09f, 0);

    [HideInInspector]
    public int midAir;


    [Header("Misc")]
    public ParticleSystem jumpParticle;
    private bool groundTouch;
    public AudioSource jumpAudio, coinAudio;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = false;
        midAir = 0;
        groundTouch = false;
        ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        FlipCharacter();
        Jump();
        JumpMisc();
        BufferJump();
        CoyeteTime();
        CheckIfGrounded();
        CheckWalls();
        LimitFallSpeed();
        JumpMidAir();
        LandingEffects();
        

    }

    private void FlipCharacter()
    {
        if (horizontalAxis > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horizontalAxis < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void Move()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        float speed;
        if (isGrounded)
        {
            speed = walkSpeed;
        }
        else
        {
            speed = airSpeed;
        }
        Vector3 targetVelocity = new Vector2(horizontalAxis * speed , rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocityRef, movementSmoothing);
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKey(KeyCode.W)) && (isGrounded))
        {
            midjump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jump = true;
            transform.parent = null;
            jumpParticle.Play();
            ani.SetTrigger("jump");
            jumpAudio.Play();
        }

    }
    private void JumpMidAir()
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
    private void LimitFallSpeed()
    {
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }

    private void JumpMisc()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !(Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W)))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    private void BufferJump()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && isGrounded == false)
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
                jumpAudio.Play();
            }
            if (bufferCounter > bufferMax)
            {
                bufferBool = false;
                bufferCounter = 0;
            }

        }

    }
    private void CoyeteTime()
    {
        if (isGrounded == false)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKey(KeyCode.W)) && coyBuffer < coyMax && midjump == false)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpAudio.Play();
                jump = true;
            }
            coyBuffer++;

        }
        else
        {
            coyBuffer = 0;
        }
        if (isGrounded = true && rb.velocity.y == 0)
        {
            midjump = false;
        }
    }
    private void CheckIfGrounded()
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
    private void CheckWalls()
    {
        Collider2D colliders = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, wallCollisionRadius, groundLayer);
        if (colliders != null)
        {
            onRightWall = true;
        }
        else
        {
           onRightWall = false;
        }
        Collider2D colliders2= Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, wallCollisionRadius, groundLayer);
        if (colliders2 != null)
        {
            onLeftWall = true;
        }
        else
        {
            onLeftWall = false;
        }

    }
    private void LandingEffects()
    {
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

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            coinAudio.Play();
        }
    }

}
