using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public QuestionData question;
    public bool isTriviaWaypoint;

    private DialogueManager dialogueManager;
    public Dialogue[] dialogues;
    private List<Dialogue> dialogueList;
    public bool isDialogueWaypoint;

    public bool isFinalWaypoint;

    public EnemyControl enemy;

    private Trivia trivia;

    private void Start()
    {
        trivia = FindObjectOfType<Trivia>();
    }

    public void ReachWaypoint()
    {
        if (isTriviaWaypoint)
        {
            StartTrivia();
        } else if (isDialogueWaypoint)
        {
            dialogueList = new List<Dialogue>();
            dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.SetWaypoint(this);
            foreach (Dialogue dialogue in dialogues)
            {
                dialogueList.Add(dialogue);
            }
            dialogueManager.StartDialogue(dialogueList[0]);
        } else if (isFinalWaypoint)
        {
            FindObjectOfType<GameMenuManager>().OpenWinMenu();
        }
    }

    private void StartTrivia()
    {
        trivia.SetWaypoint(this);
        trivia.ShowQuestion(question);
    }

    public void NextDialogue()
    {
        dialogueList.RemoveAt(0);
        if(dialogueList.Count > 0)
        {
            dialogueManager.StartDialogue(dialogueList[0]);
        }
        else
        {
            dialogueManager.CloseDialogue();
            FindObjectOfType<CharacterControl>().WalkToWaypoint();
            FindObjectOfType<BossControl>().DialogueFinished();
        }
    }

}