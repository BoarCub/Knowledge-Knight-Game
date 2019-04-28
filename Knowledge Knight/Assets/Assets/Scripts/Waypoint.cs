using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public QuestionData question;
    public bool isTriviaWaypoint;

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
        }
    }

    private void StartTrivia()
    {
        trivia.SetWaypoint(this);
        trivia.ShowQuestion(question);
    }

}