using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Player Attributes, Components, & Tracking Variables

    // Extra Player Tracking Variables
    private float dirX;
    private enum MovementState { idle, running, jumping, falling };

    [Header("Player Attributes")]
    [Header("---------------------------")]
    [Range(5, 15)][SerializeField] int iJumpForce;                           // Player Jump Height
    [Range(5, 25)][SerializeField] float fMovementSpeed;                     // Player Movement Speed

    [Header("Player Components")]
    [Header("---------------------------")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private BoxCollider2D bc;

    [Header("External References")]
    [Header("---------------------------")]
    [SerializeField] private LayerMask jumpableGround;

    [Header("Player Tracking Variables (Do Not Edit)")]
    [Header("---------------------------")]
    [SerializeField] private bool bIsGrounded;
    [SerializeField] private MovementState movementState;
    [SerializeField] public bool isDying;

    [Header("Knockback Stats")]
    [Header("---------------------------")]
    [SerializeField] public float kbForce;
    [SerializeField] public float kbTimer;
    [SerializeField] public float kbTotalTime;
    [SerializeField] public bool KnockFromRight;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        bIsGrounded = IsGrounded();

        if (kbTimer <= 0 && !isDying)
        {
            // Horizontal Movement
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * fMovementSpeed, rb.velocity.y);

            // Jump Movement
            if (Input.GetButtonDown("Jump") && bIsGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, iJumpForce);
            }

            // Update Animations
            UpdateAnimations();
        }
        else
        {
            if (KnockFromRight == false)
            {
                rb.velocity = new Vector2(-kbForce, kbForce);
            }
            if (KnockFromRight == true)
            {
                rb.velocity = new Vector2(kbForce, kbForce);
            }

            kbTimer -= Time.deltaTime;
        }

    }

    #region Helper Functions

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void UpdateAnimations()
    {
        MovementState state;

        // Running Animation
        if (dirX > 0f) // right
        {
            state = MovementState.running;
            sp.flipX = false;
        }
        else if (dirX < 0) // left
        {
            state = MovementState.running;
            sp.flipX = true;
        }
        else // idle
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)   // jumping
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f) // falling
        {
            state = MovementState.falling;
        }

        anim.SetInteger("MovementState", (int)state);
    }

    #endregion
}
