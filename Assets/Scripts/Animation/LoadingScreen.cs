using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private Animator Anim;

    void Start()
    {
        Anim = GetComponent<Animator>();
        
        State = States.Idle;
    }

    private States State
    {
        get {return(States)Anim.GetInteger("state"); }
        set {Anim.SetInteger("state", (int)value); }
    }

    public void SetAnimation()
    {
        State = States.Loading;
    }
}

public enum States
{
    Loading,
    Idle
}