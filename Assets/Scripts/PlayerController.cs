using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Player Attributes, Components, & Tracking Variables
    [Header("Player Attributes")]
    [Header("---------------------------")]
    [Range(5, 15)][SerializeField] int iJumpForce;                           // Player Jump Height
    [Range(5, 25)][SerializeField] float fMovementSpeed;                     // Player Movement Speed

    [Header("Player Components")]
    [Header("---------------------------")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [Header("Player Tracking Variables (Do Not Edit)")]
    [Header("---------------------------")]
    [SerializeField] private bool bIsGrounded;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        // Horizontal Movement
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * fMovementSpeed, rb.velocity.y);

        // Jump Movement
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, iJumpForce);
        }

        // Running Animation
        if (dirX > 0f)
        {
            anim.SetBool("bRunning", true);
        }
        else if (dirX < 0)
        {
            anim.SetBool("bRunning", true);
        }
        else
        {
            anim.SetBool("bRunning", false);
        }
    }

    #region Helper Functions

    #endregion
}
