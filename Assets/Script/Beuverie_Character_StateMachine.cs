using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_Character_StateMachine : MonoBehaviour
{
    public enum State
    {
        STATE_IDLE,
        STATE_WALK,
        STATE_DRINK,
        STATE_ADDICTION,
        STATE_BLACKOUT,
        STATE_FOLLOWCLICK,
    }

    public State state_; 
    public Beuverie_GameManager gm { get; private set; }
    public InputManager input { get; private set; }
    public Beuverie_PlayerManager pm { get; private set; }

    protected virtual void Start()
    {
        gm = Beuverie_GameManager.GM_instance;
        input = gm.GetComponent<InputManager>();
        pm = gm.playerManager;
    }

    protected virtual void Update()
    {
        Blackout_transition();
        Addiction_transition();
        switch (state_)
        {
            case State.STATE_IDLE:
                Idle_state();
       
                break;
            case State.STATE_WALK:
                Walk_state();
           
                break;
            case State.STATE_DRINK:
                Drink_state();
              
                break;
            case State.STATE_ADDICTION:
                Addiction_state();
              
                break;
            case State.STATE_BLACKOUT:
                Blackout_state();
                Debug.Log("Blackout");
                break;
            case State.STATE_FOLLOWCLICK:
                FollowClick_state();
                break;
        }
    }
    protected virtual void Idle_state()
    {
        
        Walk_transition();
        Drink_transition();
        FollowClick_transition();
    }
    protected virtual void Walk_state()
    {
        Idle_transition();
        Drink_transition();
        FollowClick_transition();
    }
    protected virtual void Addiction_state()
    {
        pm.agent.isStopped = false;
        if (pm.NearDrink)
        {
            state_ = State.STATE_DRINK;
        }
    }
    protected virtual void Blackout_state()
    {

    }
    protected virtual void Drink_state()
    {
        if (pm.drinked)
        {
           state_ = State.STATE_IDLE;
        }
    }
    protected virtual void FollowClick_state()
    {
        Walk_transition();
        Drink_transition();
        
        if (!pm.agent.pathPending)
        {
            if (pm.agent.remainingDistance <= pm.agent.stoppingDistance)
            {
                if (!pm.agent.hasPath || pm.agent.velocity.sqrMagnitude <= 0)
                {
                    pm.StopMouseControl();
                    state_ = State.STATE_IDLE;
                }
            }
        }
    }

    protected virtual void FollowClick_transition()
    {
        if (pm.MouseDestination)
        {

            pm.agent.isStopped = false;
            state_ = State.STATE_FOLLOWCLICK;
        }
    }
    protected virtual void Idle_transition()
    {

        if (input.Lstick.magnitude <= input.JoystickDeadzone)
        {
            state_ = State.STATE_IDLE;
        }
    }
    protected virtual void Walk_transition()
    {
        
        if (input.Lstick.magnitude > input.JoystickDeadzone)
        {
            pm.StopMouseControl();
            state_ = State.STATE_WALK;
        }
    }
    protected virtual void Addiction_transition()
    {
        if(pm.Addiction_timer_done&&!pm.NearDrink&&state_ != State.STATE_BLACKOUT)
        {
            state_ = State.STATE_ADDICTION;
        }
    }
    protected virtual void Blackout_transition()
    {
        if(pm.TauxAlcool.ToMuch())
        {
            state_ = State.STATE_BLACKOUT;
        }
    }
    protected virtual void Drink_transition()
    {
        if(input.Check.Pressed() && pm.NearDrink && !pm.TauxAlcool.ToMuch())
        {
            pm.StopMouseControl();
            state_ = State.STATE_DRINK;
        }
    }

}
