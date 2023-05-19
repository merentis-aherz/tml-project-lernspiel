using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SelectedQuiz : MonoBehaviour
{
    public QuizData EquipedQuiz;
    //public TMP_Text text;
    public int TimeLimit;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public QuizData GetQuiz() { return EquipedQuiz; }

    public void SetQuiz(QuizData data)
    {
        EquipedQuiz = data;
    }

    public float GetTimeLimit() { return TimeLimit; }

    public void SetTimeLimit(float value)
    {
        TimeLimit = (int)value;
    }
    /*void Update()
    {
        text.text = $"Quiz selected: {GetQuiz().ToSafeString()} \n" +
            $"Time selected: {(int) GetTimeLimit()}";
    }*/
}
