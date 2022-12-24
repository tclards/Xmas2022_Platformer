using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [Header("Tracking References")]
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerStats ps;

    [Header("Knockback Stats")]
    [SerializeField] private float knockbackPower;

    private void OnCollisionEnter2D(Collision2D collision)
    {if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage();
        }

    }

    // Helper Functions:  
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    private void TakeDamage()
    {
        anim.SetTrigger("TakeDamage");
        ps.TakeDamage(1);
        

        if (ps.Health == 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        rb.bodyType = RigidbodyType2D.Static;   // disable player movement 

        yield return new WaitForSeconds(1f);

        anim.SetTrigger("Death");               // play death animation

        yield return new WaitForSeconds(0.5f);

        //RestartLevel();
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
