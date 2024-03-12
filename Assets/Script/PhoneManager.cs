using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class PhoneManager : Invest_Character_State_Machine
{
    public List<GameObject> Screens;
    public enum Phone_State
    {
        STATE_HOME,
        STATE_CONTACT,
        STATE_MESSAGE,
        STATE_CALLING,
        STATE_HISTORIC,
        STATE_CALLED,
        STATE_GOOGLERESEARCH,
        STATE_TAPNUM,
        STATE_ARCHIVE,

    }
    public Phone_State phone_State_;
    CanvasManager cm;
    DialogueManager dm;

    protected override void Start()
    {
        base.Start();
        cm = Invest_GameManager.GM_instance.CanvasManager;
        dm = Invest_GameManager.GM_instance.DialogueManager;
    }

    protected override void Update()
    {
        base.Update();
        if(state_!= State.STATE_PHONE)
        {
            CloseAll();
        }
    }

    protected override void Phone_transition()
    {
        if (input.Phone.PressedDown())
        {
            stateBuffer_ = state_;
        }
        if (input.Phone.Pressed())
        {
            ActiveScreen("Home");
            state_ = State.STATE_PHONE;
        }

    }

    protected override void Phone_state()
    {
        base.Phone_state();
        switch (phone_State_)
        {
            case Phone_State.STATE_HOME:
                Home_state();
                break;
            case Phone_State.STATE_CONTACT:
                Contact_state();
                break;
            case Phone_State.STATE_MESSAGE:
                Message_state();
                break;
            case Phone_State.STATE_CALLING:
                Calling_state();
                break;
            case Phone_State.STATE_HISTORIC:
                Historic_state();
                break;
            case Phone_State.STATE_CALLED:
                Called_state();
                break;
            case Phone_State.STATE_GOOGLERESEARCH:
                GooogleResearch_state();
                break;
            case Phone_State.STATE_TAPNUM:
                Tapnum_state();
                break;
            case Phone_State.STATE_ARCHIVE:
                Archive_state();
                break;
        }
        if(phone_State_ != Phone_State.STATE_CALLING && phone_State_ != Phone_State.STATE_CALLED)
        {
            cm.DialoguePanel.SetActive(false);
        }
        else
        {
            cm.DialoguePanel.SetActive(true);
        }
    }

    protected override void FinInteraction()
    {
        base.FinInteraction();
        phone_State_ = Phone_State.STATE_CONTACT;
        ActiveScreen("Contact");
    }
    void Home_state()
    {

    }
    void Contact_state()
    {

    }
    void Message_state()
    {

    }
    void Calling_state()
    {
        cm.DialoguePanel.SetActive(true);
    }
    void Historic_state()
    {

    }
    void Called_state()
    {

    }
    void GooogleResearch_state()
    {

    }
    void Tapnum_state()
    {

    }
    void Archive_state()
    {

    }
    public void Home_transition()
    {
        phone_State_ = Phone_State.STATE_HOME;
        ActiveScreen("Home");
    }
    public void Contact_transition()
    {
        phone_State_ = Phone_State.STATE_CONTACT;
        ActiveScreen("Contact");
    }
    public void Message_transition()
    {
        phone_State_ = Phone_State.STATE_MESSAGE;
        ActiveScreen("Message");
    }
    public void Calling_transition(Chara_dialogue chara_Dialogue,int interactCount)
    {
        //FAIRE PASSER LE CHARA DIALOGUE PAR LA, IL FAUDRA VOIR APRES POUR METTRE LES INFOS DES ONCLICK EN PROG POUR POUVOIR FAIRE DES PREFABS ET NE PAS PERDRE DE LA SANITE
        phone_State_ = Phone_State.STATE_CALLING;

        dm.StartDialogueOut(chara_Dialogue,interactCount);
        ActiveScreen("Calling");
    }
    public void Historic_transition()
    {
        phone_State_ = Phone_State.STATE_HISTORIC;
        ActiveScreen("Historic");
    }
    public void Called_transition()
    {
        phone_State_ = Phone_State.STATE_CALLED;
        ActiveScreen("Called");
    }
    public void GoogleResearch_transition()
    {
        phone_State_ = Phone_State.STATE_GOOGLERESEARCH;
        ActiveScreen("GoogleResearch");
    }
    public void Tapnum_transition()
    {
        phone_State_ = Phone_State.STATE_TAPNUM;
        ActiveScreen("Tapnum");
    }
    public void Archive_transition()
    {
        phone_State_ = Phone_State.STATE_ARCHIVE;
        ActiveScreen("Archive");
    }
    
    GameObject GetScreen(string name)
    {
        foreach (var item in Screens)
        {
            if(item.name == name)
            {
                return item;
            }
        }
        return null;
    }
    void ActiveScreen(string name)
    {
        foreach (var item in Screens)
        {
            if (item.name == name)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }
    void CloseAll()
    {
        foreach(var item in Screens)
        {
            item.SetActive(false);
        }
    }
}
