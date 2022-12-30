using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] public AudioSource clickSound;

    public void StartGame()
    {
        clickSound.Play();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        clickSound.Play();

        Application.Quit();
    }

    public void OptionsMenu()
    {
        clickSound.Play();


    }

    public void OpenCredits()
    {
        clickSound.Play();


    }

}
