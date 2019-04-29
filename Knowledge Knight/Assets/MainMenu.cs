using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;

    public GameObject credits;
    private Animator creditsAnimator;

    private void Start()
    {
        creditsAnimator = credits.GetComponent<Animator>();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        creditsAnimator.SetBool("isOpen", true);
    }

    public void CreditsBack()
    {
        creditsAnimator.SetBool("isOpen", false);
    }

}
