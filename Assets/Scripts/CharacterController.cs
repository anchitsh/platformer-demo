using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public int morejumps;
    private Rigidbody2D rb;
    bool jump;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float rememberGroundedFor;
    float lastTimeGrounded;
    public int defaultAdditionalJumps = 1;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private int buffercounter = 0, coybuffer = 0;
    public int buffermax = 10, coymax = 10;
    bool bufferbool;
    bool midjump;
    Collider2D body;
    bool isclimbing = false;
    float gravityScaleAtStart;

    //Animator animator;
    float midair;
    bool walltouch;

    public static bool move;
    private Transform m_currMovingPlatform;


    public float walk_speed;
    public float y_max;

    public float horizontalAxis;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        morejumps = defaultAdditionalJumps;
        body = rb.GetComponent<Collider2D>();
        gravityScaleAtStart = rb.gravityScale;
        // animator = GetComponent<Animator>();
        // animator.SetBool("jump", false);
        jump = false;
        midair = 0;

    }
    private void FixedUpdate()
    {
        movestatic();
    }
    // Update is called once per frame
    void Update()
    {



        Jump();
        BetterJump();
        bufferjump();
        coyetetime();
        if (isGrounded = true && rb.velocity.y == 0)
        {
            midjump = false;
        }
        // Debug.Log(midjump + "midjump");
        //Debug.Log("grounded" + isGrounded);
        CheckIfGrounded();
//        Debug.Log("grounded" + isGrounded);

        if (rb.velocity.y < y_max)
        {
            rb.velocity = new Vector2(rb.velocity.x, y_max);
        }
        mid_air();
        print("midair int"+midair);
        print("jump" + jump);
    }


    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");

        float moveBy = x * speed;

        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    void movestatic()
    {

        horizontalAxis = Input.GetAxisRaw("Horizontal");
        //print(x);
        //print("move" + move);
        if (horizontalAxis == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            move = false;

        }
        else
        {
            rb.velocity = new Vector2(horizontalAxis * walk_speed, rb.velocity.y);
            move = true;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || morejumps > 0) && walltouch == false)
        {
            midjump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            morejumps--;
            //animator.SetBool("jump", true);
            jump = true;
            transform.parent = null;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (isclimbing == true))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //animator.SetBool("jump", true);
            jump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (walltouch == true) && isGrounded == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.5f);
            //animator.SetBool("jump", true);
            jump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (walltouch == true) && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

    }



    void mid_air()
    {
        if (jump == true && rb.velocity.y > 0)
        {
            midair = 1;
        }
        else if (jump == true && rb.velocity.y < 0)
        {
            midair = -1;
        }
        else if (jump == false)
        {
            midair = 0;
        }
        //animator.SetFloat("midair", midair);

    }

    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void bufferjump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == false)
        {
            bufferbool = true;
        }
        if (bufferbool == true)
        {
            buffercounter++;
            if (isGrounded)
            {

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                midjump = true;
            }
            if (buffercounter > buffermax)
            {
                bufferbool = false;
                buffercounter = 0;
            }

        }

    }
    void coyetetime()
    {
        if (isGrounded == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && coybuffer < coymax && midjump == false)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            coybuffer++;

        }
        else
        {
            coybuffer = 0;
        }
    }
    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (colliders != null)
        {
            isGrounded = true;
            jump = false;
            morejumps = defaultAdditionalJumps;
            // climbLadder = false;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "wall")
        {
            walltouch = true;
        }
        if (other.gameObject.tag == "moving")
        {
            m_currMovingPlatform = other.gameObject.transform;
            transform.SetParent(m_currMovingPlatform);
        }
        if (other.gameObject.tag == "bounce")
        {
            rb.velocity = new Vector2(rb.velocity.x, 30);
        }

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "wall")
        {
            walltouch = false;
        }

        if (other.gameObject.tag == "moving")
        {
            transform.parent = null;
            m_currMovingPlatform = null;

        }

    }
}
