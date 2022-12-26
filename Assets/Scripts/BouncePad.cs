using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private Animator anim;
    [SerializeField] private float bounceDuration;
    [SerializeField] private float bouncePower;
    [SerializeField] private AudioSource sBounceNoise;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == player.name)
        {
            sBounceNoise.Play();

            anim.SetTrigger("BouncePadContact");

            BouncePlayer(bounceDuration, bouncePower);
        }
    }

    private void CustomReset()
    {
        anim.ResetTrigger("BouncePadContact");
    }

    // bouncepad?
    private void BouncePlayer(float bounceDuration, float bouncePower)
    {
        float fTimer = 0f;

        while (bounceDuration > fTimer)
        {
            fTimer += Time.deltaTime;

            rb.velocity = new Vector2(0, bouncePower);

        }

    }


    public IEnumerator BouncePadFunc(float bounceDuration, float bouncePower)
    {
        float fTimer = 0f;

        Vector3 bounceDirection = new Vector3(0, transform.position.y, 0);

        while (bounceDuration > fTimer)
        {
            fTimer += Time.deltaTime;

            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector3(-bounceDirection.x, -bounceDirection.y + bouncePower, transform.position.z));

        }

        yield return 0;
    }
}
