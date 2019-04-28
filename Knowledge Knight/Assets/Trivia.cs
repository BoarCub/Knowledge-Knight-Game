using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trivia : MonoBehaviour
{

    public Color normalColor;
    public Color correctColor;
    public Color incorrectColor;
    private RawImage triviaBoardImage;

    public TextMeshProUGUI questionDisplayText;

    public GameObject triviaBoard;
    private Animator triviaAnimator;

    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;

    private RoundData round;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private List<QuestionData> questionPool = new List<QuestionData>();

    private Waypoint waypoint;

    public IEnumerator AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            triviaBoardImage.color = correctColor;
        }
        else
        {
            triviaBoardImage.color = incorrectColor;
            Debug.Log("incorrect");
        }

        yield return new WaitForSeconds(1);

        triviaAnimator.SetBool("isOpen", false);

        yield return new WaitForSeconds(1);

        if (isCorrect)
        {
            StartCoroutine(waypoint.enemy.LoseBattle());
            StartCoroutine(FindObjectOfType<CharacterControl>().WinBattle());
        }
        else
        {
            StartCoroutine(waypoint.enemy.WinBattle());
            StartCoroutine(FindObjectOfType<CharacterControl>().LoseBattle());
        }

    }
    
    public void ShowQuestion(QuestionData question)
    {
        RemoveAnswerButtons();
        QuestionData questionData = question;
        questionDisplayText.text = questionData.questionText;

        for(int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);
        }

        triviaBoardImage.color = normalColor;
        triviaAnimator.SetBool("isOpen", true);
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        triviaAnimator = triviaBoard.GetComponent<Animator>();
        triviaBoardImage = triviaBoard.GetComponent<RawImage>();

        TextReader textReader = FindObjectOfType<TextReader>();
        round = textReader.ReadQuestionSet();
        RetrieveQuestions();

        triviaBoard.SetActive(true);

    }

    private void RetrieveQuestions()
    {
        foreach (QuestionData question in round.questions)
        {
            questionPool.Add(question);
        }
        
        for(int i = 0; i < questionPool.Count; i++)
        {
            QuestionData temp = questionPool[i];
            int random = Random.Range(i, questionPool.Count);
            questionPool[i] = questionPool[random];
            questionPool[random] = temp;
        }

        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint w in waypoints)
        {
            if (w.isTriviaWaypoint)
            {
                w.question = questionPool[0];
                questionPool.RemoveAt(0);
            }
        }

    }

    public void SetWaypoint(Waypoint waypoint)
    {
        this.waypoint = waypoint;
    }

}