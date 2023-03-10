using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [Header("Tracking References")]
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerStats ps;
    public PlayerController playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Trap Collisions
        if (collision.gameObject.CompareTag("Trap"))
        {
            anim.SetTrigger("TakeDamage");

            // knockback
            playerController.kbTimer = playerController.kbTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerController.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerController.KnockFromRight = false;
            }

            playerController.sDamageTaken.Play();
            ps.TakeDamage(1);

            if (ps.Health == 0)
            {
                StartCoroutine(Die());
            }
        }

        // Killbox collisions (for out of bounds)
        if (collision.gameObject.CompareTag("KillBox"))
        {
            StartCoroutine(Die());
        }
    }

    // Helper Functions:  
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerController.isDying = false;
    }

    IEnumerator Die()
    {
        playerController.sDeath.Play();
        playerController.isDying = true;
        anim.GetComponent<Animator>().speed = 0;
        rb.bodyType = RigidbodyType2D.Static;   // disable player movement 

        yield return new WaitForSeconds(1f);

        anim.GetComponent<Animator>().speed = 1;
        anim.SetTrigger("Death");               // play death animation

        yield return new WaitForSeconds(0.5f);
    }

}
