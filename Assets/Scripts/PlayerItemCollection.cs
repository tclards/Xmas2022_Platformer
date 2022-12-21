using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CoinCollectable"))
        {
            Destroy(collision.gameObject);  //destroy object


        }
    }
}
