using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMover : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator anim;

    public void SetDisplayExplanation(bool value) 
    {
        anim.SetBool("start", value);
    }
}
