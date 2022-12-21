using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollection : MonoBehaviour
{
    [SerializeField] GameObject CollectionAnimation;
    [Range(0f,10f)][SerializeField] private float WaitTime;

    [Header("Do Not Edit")]
    [Header("------------------------")]
    [SerializeField] Transform playerPOS;
    [SerializeField] private int iCoins = 0;

    private void Start()
    {
        CollectionAnimation.SetActive(false);


    }

    private void Update()
    {
        CollectionAnimation.transform.position = playerPOS.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CoinCollectable"))
        {
            
            Destroy(collision.gameObject);  //destroy object
            iCoins++;   // count coins collected

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
