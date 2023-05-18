//=============//
// Made by Max //
//=============//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private QuizMover quizMover;

    [Header("Upper Objects")]
    [SerializeField] private TMP_Text _QuizTitleText;
    [SerializeField] private TMP_Text _QuestionText, _ExplanationText;
    [SerializeField] private Sprite _QuestionSprite;
    [SerializeField] private Image _ExplanationImage;

    [Header("Lower Objects")]
    [SerializeField] private GameObject _MultipleChoiceButtonPrefab;
    [SerializeField] private GameObject _TextInputArea, _NumberInputArea, _MultipleChoiceInputArea;
    [SerializeField] private TMP_InputField _AnswerTextInputField, _AnswerNumberInputField;
    [SerializeField] private GameObject _SubmitButton, _NextButton;

    [Header("Settings")]
    [SerializeField] private Color explanationBGColorTrue;
    [SerializeField] private Color explanationBGColorFalse;
    [SerializeField] private Color multipleChoiceBGColorTrue, multipleChoiceBGColorFalse;

    [Header("Set")]
    public QuizData quizData;
    int currentQuestion = 0;

    [HideInInspector] public bool quizFinished = false;
    List<GameObject> multipleChoiceButtons = new List<GameObject>();
    int multipleChoiceSelected = -1;

    private void Start()
    {
        //Get QuizData
        SelectedQuiz selectedQuiz = FindObjectOfType<SelectedQuiz>();
        if (selectedQuiz != null)
            quizData = selectedQuiz.EquipedQuiz;

        if (quizData == null)
            throw new System.NullReferenceException("QuizData is missing!");

        _QuizTitleText.text = quizData.name;

        //On QuizData change
        DisplayQuestion(quizData.questions[currentQuestion]);
    }

    public void OnClickSubmit()
    {
        if (quizFinished)
            return;

        //Check if correct and show explanation
        (bool?, string) checkedValue = CheckAnswer(quizData.questions[currentQuestion]);
        switch (checkedValue.Item1)
        {
            case true:
                DisplaySolution(true, checkedValue.Item2);
                break;

            case false:
                DisplaySolution(false, checkedValue.Item2);
                break;

            case null:
                return;
        }

        //Swap buttons
        _SubmitButton.SetActive(false);
        _NextButton.SetActive(true);
    }

    void DisplaySolution(bool isCorrect, string explanation)
    {
        //Set explanation text
        if (explanation != null && explanation != "")
            _ExplanationText.text = explanation;
        else if (isCorrect)
            _ExplanationText.text = "Correct!";
        else
            _ExplanationText.text = "Wrong!";

        //Change explanation BG color
        _ExplanationImage.color = isCorrect ? explanationBGColorTrue : explanationBGColorFalse;

        //Chase of MulitpleChoice show correct one via BG Color
        if (quizData.questions[currentQuestion].answerType == Question.AnswerType.MultipleChoice)
        {
            multipleChoiceButtons[multipleChoiceSelected].GetComponent<Image>().color = isCorrect ? multipleChoiceBGColorTrue : multipleChoiceBGColorFalse;

            quizData.questions[currentQuestion].multipleChoiceAnswers.FindEveryCorrectMultipleChoice().ForEach(i =>
            {
                multipleChoiceButtons[i].GetComponent<Image>().color = multipleChoiceBGColorTrue;
            });
        }

        //Show explanation box
        quizMover.SetExplanationVisibility(true);
    }

    public void OnClickNext()
    {
        if (quizFinished)
            return;

        //Clears InputFields
        _AnswerTextInputField.text = "";
        _AnswerNumberInputField.text = "";

        //Hide explanation box
        quizMover.SetExplanationVisibility(false);

        currentQuestion++;

        if (currentQuestion < quizData.questions.Count)
        {
            //Next question
            _SubmitButton.SetActive(true);
            _NextButton.SetActive(false);
            DisplayQuestion(quizData.questions[currentQuestion]);
        }
        else
        {
            //End
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
            _QuestionSprite = question.image;

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

    (bool?, string) CheckAnswer(Question question)
    {
        switch (question.answerType)
        {
            case Question.AnswerType.Text:
                if (_AnswerTextInputField.text != "")
                    return question.textAnswers.CheckForText(_AnswerTextInputField.text);
                else
                    return (null, null);

            case Question.AnswerType.Number:
                if (_AnswerNumberInputField.text != "")
                    return question.numberAnswers.CheckForNumber(float.Parse(_AnswerNumberInputField.text));
                else
                    return (null, null);

            case Question.AnswerType.MultipleChoice:
                if (multipleChoiceSelected > 0)
                    return question.multipleChoiceAnswers.CheckForMultipleChoice(multipleChoiceSelected);
                else
                    return (null, null);
        }
        return (null, null);
    }
}
