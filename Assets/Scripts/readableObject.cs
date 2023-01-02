using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class readableObject : MonoBehaviour
{
    [SerializeField] GameObject Prompt;
    [SerializeField] GameObject TextBoxUI;

    private bool canActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Prompt.SetActive(true);
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Prompt.SetActive(false);
            canActivate = false;
            TextBoxUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Activate") && canActivate)
        {
            if (TextBoxUI.activeInHierarchy == true)
            {
                TextBoxUI.SetActive(false);
            }
            else if (TextBoxUI.activeInHierarchy == false)
            {
                TextBoxUI.SetActive(true);
            }
        }
    }
}
