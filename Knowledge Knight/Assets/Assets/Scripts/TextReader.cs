﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextReader : MonoBehaviour
{

    private string path = "Assets/Resources/QuestionSets/";
    public string fileName;
    public List<QuestionData> questionSet;

    public void Start()
    {

        questionSet = new List<QuestionData>();

        RoundData round = new RoundData();

        StreamReader reader = new StreamReader(path + fileName);

        string question;

        List<AnswerData> answerList;

        string text = reader.ReadLine();

        do
        {

            answerList = new List<AnswerData>();

            question = text;
            reader.ReadLine();

            string answer = reader.ReadLine();
            while(answer != "" && answer != null)
            {

                string leading = answer.Substring(0, 4);

                AnswerData newAnswer = new AnswerData
                {
                    answerText = answer.Substring(4),
                    isCorrect = leading == "(C) "
                };

                answerList.Add(newAnswer);

                answer = reader.ReadLine();
            }

            QuestionData newQuestion = new QuestionData
            {
                questionText = question,
                answers = answerList.ToArray()
            };

            questionSet.Add(newQuestion);

            text = reader.ReadLine();

        } while (text != null);

        reader.Close();

    }

}