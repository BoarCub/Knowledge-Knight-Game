using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    private Waypoint currentWaypoint;
    private Queue<string> lines;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    private Animator dialogueAnimator;

    // Start is called before the first frame update
    void Start()
    {
        lines = new Queue<string>();
        dialogueAnimator = dialogueBox.GetComponent<Animator>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        nameText.text = dialogue.name;

        lines.Clear();

        foreach (string line in dialogue.lines)
        {
            lines.Enqueue(line);
        }

        DisplayNextSentence();
        dialogueAnimator.SetBool("isOpen", true);
    }

    public void DisplayNextSentence()
    {
        if(lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = lines.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeLine(line));
    }

    private IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return null;
        }
    }

    public void CloseDialogue()
    {
        dialogueAnimator.SetBool("isOpen", false);
    }

    public void SkipDialogue()
    {

        currentWaypoint.RemoveAllDialogue();
        EndDialogue();

    }

    private void EndDialogue()
    {
        currentWaypoint.NextDialogue();
    }

    public void SetWaypoint(Waypoint waypoint)
    {
        currentWaypoint = waypoint;
    }

}