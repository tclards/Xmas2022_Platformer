using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerItemCollection : MonoBehaviour
{
    [SerializeField] GameObject CollectionAnimation;
    [SerializeField] private TextMeshProUGUI CoinCounterText;
    [SerializeField] private TextMeshProUGUI GemCounterText;
    [Range(0f,10f)][SerializeField] private float WaitTime;

    [Header("Do Not Edit")]
    [Header("------------------------")]
    [SerializeField] Transform playerPOS;
    [SerializeField] private PlayerStats ps;
    [SerializeField] private PlayerController pc;
    [SerializeField] private int iCoins = 0;
    [SerializeField] private int iGems = 0;

    private void Start()
    {
        CollectionAnimation.SetActive(false);


    }

    private void Update()
    {
        CollectionAnimation.transform.position = playerPOS.position;

        CoinCounterText.text = iCoins.ToString();
        GemCounterText.text = iGems.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Coin Collectables
        if (collision.gameObject.CompareTag("CoinCollectable"))
        {
            pc.sCoinCollect.Play(); // play pickup audio

            Destroy(collision.gameObject);  //destroy object
            iCoins++;   // count coins collected

            // Play Animation for pickup
            StartCoroutine(Wait());
        }

        if (collision.gameObject.CompareTag("GemCollectable"))
        {
            pc.sGemCollect.Play();

            Destroy(collision.gameObject);  //destroy object
            iGems++;   // count gems collected

            // Play Animation for pickup
            StartCoroutine(Wait());
        }

        // Health Pickup Collisions
        if (collision.gameObject.CompareTag("HealthPickup") && ps.Health != ps.MaxHealth)
        {
            pc.sHealthPickup.Play();

            Destroy(collision.gameObject);

            ps.Heal(1);

            // Play Animation for pickup
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        CollectionAnimation.SetActive(true);

        yield return new WaitForSeconds(WaitTime);

        CollectionAnimation.SetActive(false);
    }

}
