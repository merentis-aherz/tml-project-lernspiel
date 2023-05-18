//=============//
// Made by Max //
//=============//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMover : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator anim;

    public void SetExplanationVisibility(bool value)
    {
        anim.SetBool("start", value);
    }
}
