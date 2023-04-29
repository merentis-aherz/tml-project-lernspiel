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
        quizData.questions[0].AnswerType.Text TextType = new quizData.questions[0].AnswerType.Text;
        quizData.questions[0].AnswerType.MultipleChoice MultipleChoiceType = new quizData.questions[0].AnswerType.MultipleChoice;

        _GIFQD = GameObject.Find("Manager").GetComponent<GetInfoFromQuizData>();

        switch (quizData.answertype)
        {
            case TextType:
                _Text = true;
                _MultipleChoice = false;
                break;

            case MultipleChoiceType:
                _MultipleChoice = true;
                _Text = false;
                break;
        }

        _GIFQD.text = _Text;
        _GIFQD.multipleChoice = _MultipleChoice;
    }
}
