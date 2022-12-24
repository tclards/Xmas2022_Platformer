using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [Header("Tracking References")]
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerStats ps;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage();
        }
    }

    // Helper Functions:  
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

        yield return new WaitForSeconds(1f);

        RestartLevel();
    }
}
