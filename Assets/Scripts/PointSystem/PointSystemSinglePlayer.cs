using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystemSinglePlayer : MonoBehaviour
{
    [Header("Points")]
    public int MaxPointPerQuestion;
    int CurrentPointInQuestion;
    int WholePointsGathered;

    [Header("Time")]
    int MaxTime;
    float TimeSpend;

    void Start()
    {
        SelectedQuiz selectedQuiz = FindObjectOfType<SelectedQuiz>();
        if (selectedQuiz != null )
            MaxTime = selectedQuiz.TimeLimit;

        TimeSpend = (float)MaxTime;
    }

    void Update()
    {
        //Math by Max
        float per =  TimeSpend / (float)MaxTime;
        CurrentPointInQuestion =  (int)(MaxPointPerQuestion * per);

        Debug.Log(CurrentPointInQuestion);

        TimeSpend -= Time.deltaTime;

    }
}
