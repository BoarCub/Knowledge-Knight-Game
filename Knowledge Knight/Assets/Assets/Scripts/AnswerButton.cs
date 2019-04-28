using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{

    public AnswerData answerData;

    public void Setup(AnswerData data)
    {
        answerData = data;
        GetComponentInChildren<TextMeshProUGUI>().text = answerData.answerText;
    }

    public void HandleClick()
    {
        StartCoroutine(FindObjectOfType<Trivia>().AnswerButtonClicked(answerData.isCorrect));
    }

}
