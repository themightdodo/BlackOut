using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PostProcessManager))]
public class AddictionFeedback : Beuverie_Character_StateMachine
{
    protected override void Update()
    {
        base.Update();
        if(state_ != State.STATE_ADDICTION)
        {
            GetComponent<PostProcessManager>().PostProcessAmount = GetComponent<PostProcessManager>().BasePostProcessAmount;
        }
        GetComponent<PostProcessManager>().FPR.passMaterial.SetFloat("_Amount", GetComponent<PostProcessManager>().PostProcessAmount);
    }
    protected override void Addiction_state()
    {
        base.Addiction_state();
        if (input.Lstick.magnitude > input.JoystickDeadzone)
        {
            GetComponent<PostProcessManager>().PostProcessAmount += 0.1f*Time.deltaTime;
        }
        else
        {
            GetComponent<PostProcessManager>().PostProcessAmount = GetComponent<PostProcessManager>().BasePostProcessAmount;
        }
    }
}
