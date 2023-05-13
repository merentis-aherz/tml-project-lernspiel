using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizHandler : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private TMP_Text _QuizTitleText;
    [SerializeField] private TMP_Text _QuestionText;
    [SerializeField] private Sprite _QuestionImage;
    [SerializeField] private TMP_InputField _AnswerInputField;

    [Header("Set")]
    public QuizData quizData;
    int currentQuestion = 0;

    [Header("Info")]
    public bool quizFinished = false;

    private void Start()
    {
        _QuizTitleText.text = quizData.name;

        //On QuizData change
        DisplayQuestion(quizData.questions[currentQuestion]);
    }

    public void OnClickSubmit()
    {
        if (quizFinished)
            return;

        CheckAnswer();
        //DisplayCorrectAnswer();
        currentQuestion++;
        if (currentQuestion < quizData.questions.Count)
        {
            DisplayQuestion(quizData.questions[currentQuestion]);
        }
        else
        {
            quizFinished = true;
            print("Quiz ended");
        }
    }

    void DisplayQuestion(Question question)
    {
        _QuestionText.text = question.question;
        if (question.image != null)
            _QuestionImage = question.image;

        //Need to change the input of the answers
    }

    void CheckAnswer()
    {
        switch (quizData.questions[currentQuestion].answerType)
        {
            case Question.AnswerType.Text:
                if (quizData.questions[currentQuestion].correctAnswers.Contains(_AnswerInputField.text))
                {
                    print("Correct Answer");
                }
                else
                {
                    print("Wrong Answer");
                }
                break;

            case Question.AnswerType.MultipleChoice:
                throw new System.NotImplementedException("Multiple Choice");
                //break;
        }
    }
}
