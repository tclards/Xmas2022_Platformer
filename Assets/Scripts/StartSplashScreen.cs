using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSplashScreen : MonoBehaviour
{

    [SerializeField] private Image splashScreen;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DisableAfterWait", 3f);
    }

    private void DisableAfterWait()
    {
        splashScreen.enabled = false;
    }

}
