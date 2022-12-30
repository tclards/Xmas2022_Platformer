using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSplashScreen : MonoBehaviour
{

    [SerializeField] private Image splashScreen;
    private bool hasBeenDisabled;

    // Start is called before the first frame update
    void Update()
    {
        if (!hasBeenDisabled)
        {
            Invoke("DisableAfterWait", 3f);
            hasBeenDisabled = true;
        }
    }

    private void DisableAfterWait()
    {
        splashScreen.enabled = false;
    }

}
