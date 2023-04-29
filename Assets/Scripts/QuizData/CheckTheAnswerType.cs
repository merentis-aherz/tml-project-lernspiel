using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTheAnswerType : MonoBehaviour
{
    public bool _Text;
    public bool _MultipleChoice;

    public QuizData quizData;

    public GetInfoFromQuizData _GIFQD;

    public void Start()
    {
        _GIFQD = GameObject.Find("Manager").GetComponent<GetInfoFromQuizData>();

        switch (quizData.answertype)
        {
            case Question.AnswerType.Text:
                _Text = true;
                _MultipleChoice = false;
                break;

            case Question.AnswerType.MultipleChoice:
                _MultipleChoice = true;
                _Text = false;
                break;
        }

        _GIFQD.text = _Text;
        _GIFQD.multipleChoice = _MultipleChoice;
    }
}
