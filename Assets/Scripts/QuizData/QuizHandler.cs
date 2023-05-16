using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject _MultipleChoiceButtonPrefab;

    [Header("Set")]
    public QuizData quizData;
    int currentQuestion = 0;

    [Header("Info")]
    public bool quizFinished = false;

    List<GameObject> multipleChoiceButtons = new List<GameObject>();
    int multipleChoiceSelected = -1;

    private void Start()
    {
        //Get QuizData
        SelectedQuiz selectedQuiz = FindObjectOfType<SelectedQuiz>();
        if (selectedQuiz != null)
            quizData = selectedQuiz.EquipedQuiz;

        _QuizTitleText.text = quizData.name;

        //On QuizData change
        DisplayQuestion(quizData.questions[currentQuestion]);
    }

    public void OnClickSubmit()
    {
        if (quizFinished)
            return;

        switch (CheckAnswer())
        {
            case true:
                print("Correct Answer");
                break;

            case false:
                print("Wrong Answer");
                break;

            case null:
                return;
        }

        //Clears InputFields
        _AnswerTextInputField.text = "";
        _AnswerNumberInputField.text = "";

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
        //Text
        _QuestionText.text = question.question;

        //Image
        if (question.image != null)
            _QuestionImage = question.image;

        //Areas
        _TextInputArea.SetActive(question.answerType == Question.AnswerType.Text);
        _NumberInputArea.SetActive(question.answerType == Question.AnswerType.Number);
        bool isMultipleChoice = question.answerType == Question.AnswerType.MultipleChoice;
        _MultipleChoiceInputArea.SetActive(isMultipleChoice);

        //Clear buttons for multiple choice
        foreach (GameObject g in multipleChoiceButtons)
            Destroy(g);
        multipleChoiceButtons = new List<GameObject>();
        multipleChoiceSelected = -1;

        //Add new buttons for multiple choice
        if (isMultipleChoice)
        {
            for (int i = 0; i < question.multipleChoiceAnswers.Count; i++)
            {
                GameObject newButton = Instantiate(_MultipleChoiceButtonPrefab, _MultipleChoiceInputArea.transform);
                int selected = i; //Must set new variable
                newButton.GetComponent<Button>().onClick.AddListener(delegate { OnClickMultipleChoiceButton(selected); });
                newButton.transform.Find("Text").GetComponent<TMP_Text>().text = question.multipleChoiceAnswers[i].value;
                multipleChoiceButtons.Add(newButton);
            }
        }
    }

    void OnClickMultipleChoiceButton(int selected)
    {
        //Disable / Enable Seletion
        for (int i = 0; i < multipleChoiceButtons.Count; i++)
            multipleChoiceButtons[i].transform.Find("Selection").GetComponent<CanvasGroup>().alpha = i == selected ? 1 : 0;

        //Set
        multipleChoiceSelected = selected;
    }

    bool? CheckAnswer()
    {
        switch (quizData.questions[currentQuestion].answerType)
        {
            case Question.AnswerType.Text:
                if (_AnswerTextInputField.text != "")
                    return quizData.questions[currentQuestion].textAnswers.CheckForText(_AnswerTextInputField.text);
                else
                    return null;

            case Question.AnswerType.Number:
                if (_AnswerNumberInputField.text != "")
                    return quizData.questions[currentQuestion].numberAnswers.CheckForNumber(float.Parse(_AnswerNumberInputField.text));
                else
                    return null;

            case Question.AnswerType.MultipleChoice:
                if (multipleChoiceSelected > 0)
                    return quizData.questions[currentQuestion].multipleChoiceAnswers.CheckForMultipleChoice(multipleChoiceSelected);
                else
                    return null;
        }
        return null;
    }
}
