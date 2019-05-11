using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;

    public GameObject credits;
    private Animator creditsAnimator;

    public GameObject instructions;
    private Animator instructionsAnimator;
    public GameObject[] instructionPanels;
    private int instructionsIndex = 0;

    private void Start()
    {
        creditsAnimator = credits.GetComponent<Animator>();
        instructionsAnimator = instructions.GetComponent<Animator>();
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

    public void Instructions()
    {
        instructionsIndex = 0;
        instructionPanels[0].SetActive(true);

        for(int i = 1; i < instructionPanels.Length; i++)
        {
            instructionPanels[i].SetActive(false);
        }

        instructionsAnimator.SetBool("isOpen", true);
    }

    public void InstructionsNext()
    {
        Debug.Log("Continue");
        if(instructionsIndex < instructionPanels.Length - 1)
        {
            instructionPanels[instructionsIndex].SetActive(false);
            instructionsIndex++;
            instructionPanels[instructionsIndex].SetActive(true);
        }
        else
        {
            InstructionsBack();
        }
    }

    public void InstructionsBack()
    {
        Debug.Log("Back");
        instructionsAnimator.SetBool("isOpen", false);
    }

}
