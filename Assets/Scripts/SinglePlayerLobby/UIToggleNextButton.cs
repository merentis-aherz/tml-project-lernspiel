using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIToggleNextButton : MonoBehaviour
{
    public Button nextButton;
    public TMP_Text text;
    public SelectedQuiz selectedQuiz;

    void Update()
    {
        if (selectedQuiz != null)
        {
            if (selectedQuiz.EquipedQuiz != null) 
            {
                text.text = "Next";
                nextButton.enabled = true;
            }
            else
            {
                text.text = "Choose an quiz";
                nextButton.enabled = false;
            }
        } 
        else
        {
            text.text = "Choose an quiz";
            nextButton.enabled = false;
        }
    }
}
