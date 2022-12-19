using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Player Attributes, Components, & Tracking Variables
    [Header("Player Attributes")]
    [Header("---------------------------")]
    [Range(5, 15)][SerializeField] int iJumpForce;                           // Player Jump Height
    [Range(1, 3)][SerializeField] int iJumpsAllowed;                         // Number of jumps allowed

    [Header("Player Components")]
    [Header("---------------------------")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Player Tracking Variables (Do Not Edit)")]
    [Header("---------------------------")]
    [SerializeField] private int iTimesJumped;
    [SerializeField] private bool bIsGrounded = false;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        // Movement Code
        bIsGrounded = PlayerGrounded();   // Helper function to determine when player is on ground vs in the air
        if (bIsGrounded)
        {
            iTimesJumped = 0;
        }

        if (Input.GetButtonDown("Jump") && iTimesJumped < iJumpsAllowed) // Jump - Space Key
        {
            iTimesJumped++;

            rb.velocity = new Vector3(0, iJumpForce, 0);
        }



    }

    #region Helper Functions
    private bool PlayerGrounded()       // Determines if player is on ground or if player is falling/jumping
    {
        if (rb.velocity.y == 0)
        {
            bIsGrounded = true;
        }
        else if (rb.velocity.y != 0)
        {
            bIsGrounded = false;
        }

        return bIsGrounded;
    }

    #endregion
}
