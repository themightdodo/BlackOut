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

    public bool triggerDialogue;

    public Timer PhoneClose { get; set; }

    protected virtual void Start()
    {
        gm = Invest_GameManager.GM_instance;
        pm = gm.playerManager;
        input = gm.GetComponent<InputManager>();
        PhoneClose = new Timer(0.2f);
        pm.Focus.AddListener(Focus);
        pm.MiniJeu.AddListener(MiniGame_transition);
        pm.FinMiniJeu.AddListener(IdlePls);
        pm.FinInteraction.AddListener(FinInteraction);
        pm.TriggerDialogue.AddListener(TriggerDialogue);
       
    }

    protected virtual void LateUpdate()
    {
        if (!pm.StartEnd.Done())
        {
            return;
        }
        PhoneClose.Refresh();
        switch (state_)
        {
            case State.STATE_IDLE:
                
                Idle_state();
                break;
            case State.STATE_WALK:
             
                Walk_state();
                break;
            case State.STATE_TALK:
               
                Talk_state();
                break;
            case State.STATE_EXAMIN:
               
                Examin_state();
                break;
            case State.STATE_SHOW:
         
                Show_state();
                break;
            case State.STATE_PHONE:
        
                Phone_state();
                break;
            case State.STATE_MINIGAME:
           
                MiniGame_state();
                break;
        }
    }

    protected virtual void Idle_state()
    {
        Walk_transition();

        Phone_transition();
    }
    protected virtual void Walk_state()
    {
        Idle_transition();

        Phone_transition();
    }
    protected virtual void TriggerDialogue()
    {
        triggerDialogue = true;
        Talk_transition();
        
    }
    protected virtual void Focus()
    {     
        if(pm.Interaction_cooldown.Done()&&pm.throwingItem.Done())
        {
            if (pm.Current_Focus_Object.CompareTag("Interactible"))
            {
                Show_transition();
                Talk_transition();
            }
            else if (input.Talk.PressedDown()&&pm.Current_Focus_Object.CompareTag("Phone"))
            {
                pm.PhoneActive = true;
                Destroy(pm.Current_Focus_Object);
                pm.Current_Focus_Object = null;
            }
            else if (input.Talk.PressedDown()&& pm.Current_Focus_Object.GetComponent<Interactible>().HandVersion != null)
            {
                Pick();
            }
            if (pm.Current_Focus_Object.GetComponent<Interactible>().chara_Dialogue != null)
            {
                Examin_transition();
            } 
           
        }
    }
    protected virtual void Pick()
    {

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
        

        if ((input.Cancel.Pressed()||input.Phone.PressedDown()))
        {
            PhoneClose.Reset();
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
        if (state_ == State.STATE_PHONE||!pm.PhoneActive||!PhoneClose.Done())
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
        if (input.Check.PressedDown() || triggerDialogue)
        {
            state_ = State.STATE_EXAMIN;
        }
    }
    protected virtual void Talk_transition()
    {
        if (input.Talk.PressedDown()||triggerDialogue)
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
    void IdlePls()
    {
        state_ = State.STATE_IDLE;
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
        triggerDialogue = false;
        if (state_ != State.STATE_PHONE)
        {
            state_ = State.STATE_IDLE;
        }
        
    }
}
