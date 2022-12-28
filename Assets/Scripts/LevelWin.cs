using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {

    }

}
