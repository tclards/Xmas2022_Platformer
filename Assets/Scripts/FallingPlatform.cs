using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Components")]
    [Header("-----------------------")]
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] AudioSource dropSound;

    [Header("Destination")]
    [Header("-----------------------")]
    [SerializeField] GameObject fallToHere;

    [Header("Stats")]
    [Header("-----------------------")]
    [SerializeField] float fallSpeed;
    [SerializeField] int iWaitForDropTime;

    private bool bIsFalling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if player lands on platform and the platform is not already falling
        if (collision.gameObject.name == "Player_01" && !bIsFalling)
        {
            bIsFalling = true;

            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        // Wait for moment
        yield return new WaitForSeconds(iWaitForDropTime);

        // play sound & disable anim
        dropSound.Play();
        anim.enabled = false;

        yield return new WaitForSeconds(2);

        // begin fall
        Destroy(gameObject);
    }
}
