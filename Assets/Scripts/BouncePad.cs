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
    //[SerializeField] private bool horizontalPad;
    //[SerializeField] private bool RightBounce;

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

            //if (horizontalPad == true)
            //{
            //    BouncePlayerToSide(bounceDuration, bouncePower);
            //}
            //else
            //{
            //    BouncePlayer(bounceDuration, bouncePower);
            //}
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

            rb.velocity = new Vector2(rb.velocity.x, bouncePower);

        }
    }

    //private void BouncePlayerToSide(float bounceDuration, float bouncePower)
    //{
    //    float fTimer = 0f;

    //    while (bounceDuration > fTimer)
    //    {
    //        fTimer += Time.deltaTime;

    //        if (RightBounce)
    //        {
    //            rb.AddForce(new Vector2(-bouncePower * rb.velocity.x, rb.velocity.y), ForceMode2D.Impulse);
    //        }
    //        else
    //        {
    //            rb.AddForce(new Vector2(bouncePower * rb.velocity.x, rb.velocity.y), ForceMode2D.Impulse);
    //        }

    //    }
    //}
}
