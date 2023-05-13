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
    [SerializeField] private GameObject _TextInputArea, _NumberInputArea, _MultipleChoiceInputArea;
    [SerializeField] private TMP_InputField _AnswerTextInputField, _AnswerNumberInputField;

    [Header("Set")]
    public QuizData quizData;
    int currentQuestion = 0;

    [Header("Info")]
    public bool quizFinished = false;

    private void Start()
    {
        ///Get QuizData

        _QuizTitleText.text = quizData.name;

        //On QuizData change
        DisplayQuestion(quizData.questions[currentQuestion]);
    }

    public void OnClickSubmit()
    {
        if (quizFinished)
            return;

        CheckAnswer();
        ///DisplayCorrectAnswer();
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

        _TextInputArea.SetActive(quizData.questions[currentQuestion].answerType == Question.AnswerType.Text);
        _NumberInputArea.SetActive(quizData.questions[currentQuestion].answerType == Question.AnswerType.Number);
        _MultipleChoiceInputArea.SetActive(quizData.questions[currentQuestion].answerType == Question.AnswerType.MultipleChoice);
    }

    void CheckAnswer()
    {
        switch (quizData.questions[currentQuestion].answerType)
        {
            case Question.AnswerType.Text:
                if (quizData.questions[currentQuestion].textAnswers.CheckForText(_AnswerTextInputField.text))
                {
                    print("Correct Answer");
                }
                else
                {
                    print("Wrong Answer");
                }
                break;

            case Question.AnswerType.Number:
                if (quizData.questions[currentQuestion].numberAnswers.CheckForNumber(float.Parse(_AnswerNumberInputField.text)))
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
                if (quizData.questions[currentQuestion].multipleChoiceAnswers.CheckForMultipleChoice(0))
                {
                    print("Correct Answer");
                }
                else
                {
                    print("Wrong Answer");
                }
                break;
        }
    }
}
