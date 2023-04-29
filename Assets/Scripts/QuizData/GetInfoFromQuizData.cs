using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetInfoFromQuizData : MonoBehaviour
{
    public TMP_Text QuestionText;
    public TMP_InputField AnswerInputField;
    public Button SubmitButton;
    public QuizData[] quizData;

    [HideInInspector]
    public int CurrentQuestion;
    public int HowManyQuestions;

    public bool QuizIsDone;

    public bool text;
    public bool multipleChoice;

    void Start()
    {
        //sets variables
        CurrentQuestion = 0;
        HowManyQuestions = quizData[0].questions[CurrentQuestion].answers[0].Length;
        QuestionText.text = quizData[0].questions[CurrentQuestion].question;
    }

    public void Submitted()
    {
        //Submitting the answer
        if (!QuizIsDone)
        {
            if (text)
            {
                if (AnswerInputField.text == quizData[0].questions[CurrentQuestion].answers[0])
                {
                    Debug.Log("Right");
                }
                else
                {
                    Debug.Log("Wrong");
                }
            }

            CurrentQuestion++;
            if (CurrentQuestion <= HowManyQuestions)
            {
                QuestionText.text = quizData[0].questions[CurrentQuestion].question;
            }
            else
            {
                Debug.Log("QuizDone");
                QuizIsDone = true;
            }
        }
        else
        {
            Debug.Log("QuizDone");
        }
    }
}
