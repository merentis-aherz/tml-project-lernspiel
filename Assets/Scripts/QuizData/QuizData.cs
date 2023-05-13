using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuizData", menuName = "QuizData/QuizData")]
public class QuizData : ScriptableObject
{
    //Idea for no Autocorrection, but Teacher corrects it. (Like a Test)
    public List<Question> questions;
}

[System.Serializable]
public class Question
{
    public enum AnswerType { Text, Number, MultipleChoice }

    public string question;
    public Sprite image;
    public AnswerType answerType;

    public List<TextAnswer> textAnswers;
    [TextArea(1, 5)] public string textWrongExplanation;

    public List<NumberAnswer> numberAnswers;

    public List<MultipleChoiceAnswer> multipleChoiceAnswers;


    [System.Serializable]
    public class TextAnswer
    {
        public bool correct = true;
        public string value;
        public bool capitalSpecific;
    }

    [System.Serializable]
    public class NumberAnswer
    {
        public enum CompareType { Equal, Less, Greater, EqualOrLess, EqualOrGreater, NotEqual }
        public bool correct = true;
        public CompareType compareType;
        public float value;
    }

    [System.Serializable]
    public class MultipleChoiceAnswer
    {
        public bool correct = true;
        public string value;
        [TextArea(1, 5)] public string wrongExplanation;
    }
}

public static class QuestionClass
{
    public static bool CheckForText(this List<Question.TextAnswer> textAnswers, string input)
    {
        foreach (Question.TextAnswer t in textAnswers)
            if (t.capitalSpecific ? input == t.value : input.ToLower() == t.value.ToLower())
                return t.correct;
        return false;
    }

    public static bool CheckForNumber(this List<Question.NumberAnswer> numberAnswers, float input)
    {
        foreach (Question.NumberAnswer n in numberAnswers)
            switch (n.compareType)
            {
                case Question.NumberAnswer.CompareType.Equal:
                    if (input == n.value)
                        return n.correct;
                    break;
                case Question.NumberAnswer.CompareType.Less:
                    if (input < n.value)
                        return n.correct;
                    break;
                case Question.NumberAnswer.CompareType.Greater:
                    if (input > n.value)
                        return n.correct;
                    break;
                case Question.NumberAnswer.CompareType.EqualOrLess:
                    if (input <= n.value)
                        return n.correct;
                    break;
                case Question.NumberAnswer.CompareType.EqualOrGreater:
                    if (input >= n.value)
                        return n.correct;
                    break;
                case Question.NumberAnswer.CompareType.NotEqual:
                    if (input != n.value)
                        return n.correct;
                    break;
            }
        return false;
    }

    public static bool CheckForMultipleChoice(this List<Question.MultipleChoiceAnswer> multipleChoiceAnswers, int input)
    {
        return multipleChoiceAnswers[input].correct;
    }
}