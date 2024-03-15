using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Invest_Character_State_Machine : MonoBehaviour
{
    public enum State
    {
        STATE_IDLE,
        STATE_WALK,
        STATE_TALK,
        STATE_EXAMIN,
        STATE_SHOW,
        STATE_PHONE,
        STATE_MINIGAME,
    }

    public State state_;
    public State stateBuffer_;

    public Invest_GameManager gm {get; set;}
    public Invest_PlayerManager pm { get; set; }
    public InputManager input { get; set; }

    protected virtual void Start()
    {
        gm = Invest_GameManager.GM_instance;
        pm = gm.playerManager;
        input = gm.GetComponent<InputManager>();

        pm.Focus.AddListener(Focus);
        pm.MiniJeu.AddListener(MiniGame_transition);
        pm.FinInteraction.AddListener(FinInteraction);
    }

    protected virtual void Update()
    {
        Phone_transition();
        switch (state_)
        {
            case State.STATE_IDLE:
                Debug.Log("Idle");
                Idle_state();
                break;
            case State.STATE_WALK:
                Debug.Log("Walk");
                Walk_state();
                break;
            case State.STATE_TALK:
                Debug.Log("Talk");
                Talk_state();
                break;
            case State.STATE_EXAMIN:
                Debug.Log("Examin");
                Examin_state();
                break;
            case State.STATE_SHOW:
                Debug.Log("Show");
                Show_state();
                break;
            case State.STATE_PHONE:
                Debug.Log("Phone");
                Phone_state();
                break;
            case State.STATE_MINIGAME:
                Debug.Log("Minigame");
                MiniGame_state();
                break;
        }
    }

    protected virtual void Idle_state()
    {
        Walk_transition();
    }
    protected virtual void Walk_state()
    {
        Idle_transition();
    }
    protected virtual void Focus()
    {
       
        if(pm.Interaction_cooldown.Done()&&pm.throwingItem.Done())
        {
            Show_transition();
            Talk_transition();
            Examin_transition();
        }
           
        

    }
    protected virtual void Talk_state()
    {

    }
    protected virtual void Examin_state()
    {
        if (input.Cancel.Pressed())
        {
            state_ = State.STATE_IDLE;
        }
    }
    protected virtual void Show_state()
    {
        if (input.Cancel.Pressed())
        {
            state_ = State.STATE_IDLE;
        }
    }
    protected virtual void Phone_state()
    {
        if (input.Cancel.Pressed())
        {
            state_ = stateBuffer_;
        }
    }
    protected virtual void MiniGame_state()
    {
        if (input.Cancel.Pressed())
        {
            state_ = State.STATE_IDLE;
        }
    }
    protected virtual void MiniGame_transition()
    {
        state_ = State.STATE_MINIGAME;
    }
    protected virtual void Phone_transition()
    {
        if (state_ == State.STATE_PHONE)
        {
            return;
        }
        if (input.Phone.PressedDown())
        {
            stateBuffer_ = state_;      
        }
        if (input.Phone.Pressed())
        {
            state_ = State.STATE_PHONE;
        }
    }
    protected virtual void Show_transition()
    {
        if (input.Inventory.Pressed())
        {
            state_ = State.STATE_SHOW;
        }
    }
    protected virtual void Examin_transition()
    {
        if (input.Check.Pressed())
        {
            state_ = State.STATE_EXAMIN;
        }
    }
    protected virtual void Talk_transition()
    {
        if (input.Talk.Pressed())
        {
            state_ = State.STATE_TALK;
        }
    }
    protected virtual void Walk_transition()
    {
        if(input.Lstick.magnitude > input.JoystickDeadzone)
        {
            state_ = State.STATE_WALK;
        }
    }
    protected virtual void Idle_transition()
    {
        if (input.Lstick.magnitude < input.JoystickDeadzone)
        {
            state_ = State.STATE_IDLE;
        }
    }
    protected virtual void FinInteraction()
    {
        if(state_ != State.STATE_PHONE)
        {
            state_ = State.STATE_IDLE;
        }
        
    }
}
