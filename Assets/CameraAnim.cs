using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraAnim : Beuverie_Character_StateMachine
{
    Animator animator;
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

    }
    protected override void Update()
    {
        base.Update();
        if (state_ != State.STATE_ADDICTION)
        {
            animator.Play("Idle");
        }
        
    }
    protected override void Addiction_state()
    {
        base.Addiction_state();
        if (input.Lstick.magnitude > input.JoystickDeadzone)
        {
            animator.Play("Shake");
        }
        else
        {
            animator.Play("Idle");
        }
    }
}
