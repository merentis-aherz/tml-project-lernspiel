using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetInfoFromQuizData : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_InputField answerInputField;
    public Button submitButton;
    public QuizData[] quizData;

    [HideInInspector]
    public int currentQuestion;
    public int howManyQuestions;

    public bool quizIsDone;

    public bool text;
    public bool multipleChoice;

    void Start()
    {
        //sets variables
        currentQuestion = 0;
        howManyQuestions = quizData[0].questions[currentQuestion].answers[0].Length;
        questionText.text = quizData[0].questions[currentQuestion].question;
    }

    public void Submitted()
    {
        //Submitting the answer
        if (!quizIsDone)
        {
            if (text)
            {
                if (answerInputField.text == quizData[0].questions[currentQuestion].answers[0])
                {
                    Debug.Log("Right");
                }
                else
                {
                    Debug.Log("Wrong");
                }
            }

            currentQuestion++;
            if (currentQuestion <= howManyQuestions)
            {
                questionText.text = quizData[0].questions[currentQuestion].question;
            }
            else
            {
                Debug.Log("QuizDone");
                quizIsDone = true;
            }
        }
        else
        {
            Debug.Log("QuizDone");
        }
    }
}
