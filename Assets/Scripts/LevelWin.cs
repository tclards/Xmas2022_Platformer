using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    AudioSource finishSound;

    // Start is called before the first frame update
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            finishSound.Play();
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static; 
            rb.velocity = Vector3.zero;
            Invoke("CompleteLevel", 2f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
