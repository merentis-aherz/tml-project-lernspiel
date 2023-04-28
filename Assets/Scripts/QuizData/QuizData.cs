using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuizData", menuName = "QuizData/QuizData")]
public class QuizData : ScriptableObject
{
    public string quizName;
    public List<Question> questions;
}

[System.Serializable]
public class Question
{
    public enum AnswerType { Text, MultipleChoice }

    public string question;
    public Sprite image;
    public AnswerType answertype;
    public string[] answers;
}
