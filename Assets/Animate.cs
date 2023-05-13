using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    public Animator anim;

    public void SetBoolTrue()
    {
        anim.SetBool("GoDown", true);
    }

    public void SetBoolFalse()
    {
        anim.SetBool("GoDown", false);
    }
}
