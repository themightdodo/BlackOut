using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_PlayerAudio : Beuverie_Character_StateMachine
{
    AudioManager audioManager;
    bool pasPlayed;

    protected override void Start()
    {
        base.Start();
        audioManager = gm.GetComponent<AudioManager>();
    }

    protected override void Drink_state()
    {
        base.Drink_state();
        audioManager.Stop("Pas");
        pasPlayed = false;
    }
    protected override void Drink_transition()
    {
        if (input.Check.Pressed() && pm.NearDrink && !pm.TauxAlcool.ToMuch())
        {
            audioManager.Play("Boit");
            pm.StopMouseControl();
            state_ = State.STATE_DRINK;
        }
    }
    protected override void FollowClick_state()
    {
        Walk_transition();
        Drink_transition();
        if (!pm.agent.pathPending)
        {

            if (!pm.agent.hasPath)
            {
                pm.StopMouseControl();
                audioManager.Stop("Pas");
                state_ = State.STATE_IDLE;
            }

        }
    }
    protected override void Idle_transition()
    {
        if (input.Lstick.magnitude <= input.JoystickDeadzone&&state_!=State.STATE_ADDICTION)
        {
            audioManager.Stop("Pas");
            state_ = State.STATE_IDLE;
        }
    }
    protected override void Walk_transition()
    {
        if (input.Lstick.magnitude > input.JoystickDeadzone && state_ != State.STATE_ADDICTION)
        {
            audioManager.Play("Pas");
            pm.StopMouseControl();
            state_ = State.STATE_WALK;
        }
    }
    protected override void Addiction_state()
    {
        pm.agent.isStopped = false;
        if (pm.NearDrink)
        {
            audioManager.Play("Boit");
            state_ = State.STATE_DRINK;
        }


    }
    
    protected override void Addiction_transition()
    {
  
       
        if (pm.Addiction_timer_done && !pm.NearDrink && state_ != State.STATE_BLACKOUT && state_ != State.STATE_DRINK)
        {
            if (pasPlayed == false)
            {
                audioManager.Play("Pas");
                pasPlayed = true;
            }

            state_ = State.STATE_ADDICTION;
        }
        else if (pm.Addiction_timer_done && pm.NearDrink && state_ != State.STATE_BLACKOUT && state_ != State.STATE_DRINK)
        {
            if (pasPlayed == false)
            {
                audioManager.Play("Pas");
                pasPlayed = true;
            }

            state_ = State.STATE_ADDICTION;
        }
    }
    protected override void FollowClick_transition()
    {
        if (pm.MouseDestination)
        {
            audioManager.Play("Pas");
            pm.agent.isStopped = false;
            state_ = State.STATE_FOLLOWCLICK;
        }
    }
}
