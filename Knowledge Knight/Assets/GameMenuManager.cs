using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{

    public GameObject winMenu;
    private Animator winMenuAnimator;

    public GameObject loseMenu;
    private Animator loseMenuAnimator;

    // Start is called before the first frame update
    void Start()
    {
        winMenuAnimator = winMenu.GetComponent<Animator>();
        loseMenuAnimator = loseMenu.GetComponent<Animator>();
    }

    public void OpenWinMenu()
    {
        winMenuAnimator.SetBool("isOpen", true);
    }

    public void OpenLoseMenu()
    {
        loseMenuAnimator.SetBool("isOpen", true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}