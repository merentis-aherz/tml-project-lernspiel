using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISelectedQuizText : MonoBehaviour
{
    public SelectedQuiz selectedQuiz;
    public TMP_Text text;

    void Update()
    {
        if (selectedQuiz != null)
        {
            if (selectedQuiz.EquipedQuiz != null)
            {
                if (selectedQuiz.TimeLimit != 0)
                {
                    text.text = $"Quiz selected: \n{selectedQuiz.GetQuiz().name} \n\nTime limited: \n{(int)selectedQuiz.GetTimeLimit()} seconds";
                }
                else
                {
                    text.text = $"Quiz selected: \n{selectedQuiz.GetQuiz().name} \n\nTime limited: \nunlimited!";
                }
            }
            else
            {
                text.text = "No quiz selected!";
            }
        }
        else
        {
            text.text = "No quiz selected!";
        }
    }
}
