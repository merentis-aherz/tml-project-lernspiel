using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISecondsText : MonoBehaviour
{
    public TMP_Text text;
    public Slider slider;

    void Update()
    {
        text.text = slider.value.ToString() + " Seconds";
    }
}
