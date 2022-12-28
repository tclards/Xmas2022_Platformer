using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Player Attributes, Components, & Tracking Variables

    // Extra Player Tracking Variables
    private float dirX;
    private float dirY;
    private enum MovementState { idle, running, jumping, falling };

    [Header("Player Attributes")]
    [Header("---------------------------")]
    [Range(5, 15)][SerializeField] int iJumpForce;                           // Player Jump Height
    [Range(5, 25)][SerializeField] float fMovementSpeed;                     // Player Movement Speed
    [SerializeField] private float dashingVelocity;
    [SerializeField] private float dashingTime;
    private Vector2 dashingDir;


    [Header("Player Components")]
    [Header("---------------------------")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private TrailRenderer trailRender;

    [Header("External References")]
    [Header("---------------------------")]
    [SerializeField] private LayerMask jumpableGround;

    [Header("Player Tracking Variables (Do Not Edit)")]
    [Header("---------------------------")]
    [SerializeField] private bool bIsGrounded;
    [SerializeField] private MovementState movementState;
    [SerializeField] public bool isDying;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool xFlip;

    [Header("Knockback Stats")]
    [Header("---------------------------")]
    [SerializeField] public float kbForce;
    [SerializeField] public float kbTimer;
    [SerializeField] public float kbTotalTime;
    [SerializeField] public bool KnockFromRight;

    [Header("SFX")]
    [Header("---------------------------")]
    [SerializeField] private AudioSource sJump;
    [SerializeField] public AudioSource sCoinCollect;
    [SerializeField] public AudioSource sDamageTaken;
    [SerializeField] public AudioSource sDeath;
    [SerializeField] public AudioSource sHealthPickup;
    [SerializeField] private AudioSource sSecretFound;
    [SerializeField] public AudioSource sGemCollect;

    #endregion

    // Update is called once per frame
    private void Update()
    {
        bIsGrounded = IsGrounded();

        if (kbTimer <= 0 && !isDying)
        {
            // Horizontal Movement
            dirX = Input.GetAxisRaw("Horizontal");
            dirY = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(dirX * fMovementSpeed, rb.velocity.y);

            // Jump Movement
            if (Input.GetButtonDown("Jump") && bIsGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, iJumpForce);
                sJump.Play();
            }

            //dash
            if (Input.GetButtonDown("Dash") && canDash)
            {
                isDashing = true;
                canDash = false;
                trailRender.emitting = true;

                dashingDir = new Vector2(dirX, dirY).normalized;
                if (dashingDir == Vector2.zero)
                {
                    if (xFlip)
                    {
                        dashingDir = new Vector2(-transform.localScale.x, 0);
                    }
                    else if (!xFlip)
                    {
                        dashingDir = new Vector2(transform.localScale.x, 0);
                    }
                }

                // stop dash
                StartCoroutine(StopDashing());
            }
            if (isDashing)
            {
                rb.velocity = dashingDir.normalized * dashingVelocity;
                return;
            }
            if (bIsGrounded)
            {
                canDash = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SecretFoundHitBox"))
        {
            sSecretFound.Play();
            Destroy(collision.gameObject);
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
            xFlip = false;
        }
        else if (dirX < 0) // left
        {
            state = MovementState.running;
            sp.flipX = true;
            xFlip = true;
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

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);

        trailRender.emitting = false;
        isDashing = false;
    }

    #endregion
}
