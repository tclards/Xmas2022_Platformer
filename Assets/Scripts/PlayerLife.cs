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

            ps.TakeDamage(1);

            if (ps.Health == 0)
            {
                StartCoroutine(Die());
            }
        }
    }

    // Helper Functions:  
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Die()
    {
        rb.bodyType = RigidbodyType2D.Static;   // disable player movement 

        yield return new WaitForSeconds(1f);

        anim.SetTrigger("Death");               // play death animation

        yield return new WaitForSeconds(0.5f);
    }


    


    // bouncepad?
    //IEnumerator Knockback(float knockbackDuration, float knockbackPower, Vector3 knockbackDirection)
    //{
    //    float fTimer = 0f;

    //    while (knockbackDuration > fTimer)
    //    {
    //        fTimer += Time.deltaTime;

    //        rb.velocity = new Vector2(0, 0);
    //        rb.AddForce(new Vector3(-knockbackDirection.x, -knockbackDirection.y + knockbackPower, transform.position.z));

    //    }

    //    yield return 0;
    //}

}
