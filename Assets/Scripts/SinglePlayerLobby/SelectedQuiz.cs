using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedQuiz : MonoBehaviour
{
    public QuizData EquipedQuiz;
    public float TimeLimit;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetQuiz(QuizData data)
    {
        EquipedQuiz = data;
    }

    public void SetTimeLimit(float value)
    {
        TimeLimit = value;
    }
}