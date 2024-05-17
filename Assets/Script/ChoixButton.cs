using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixButton : MonoBehaviour
{
    public Dialogue dialogue;
    public Choix choix;
    DialogueManager dm;
    Timer delay;

    private void Start()
    {
        dm = Invest_GameManager.GM_instance.DialogueManager;
        delay = new Timer(0.5f);
        delay.CurrentValue = 0;
    }
    public void TriggerDialogue()
    {
        if (!delay.Done())
        {
            delay.Refresh();
            return;
        }

        if ((dm.pm.Current_Focus_Object != null && !dm.pm.Current_Focus_Object.GetComponent<Interactible>().noChoiceRegister)||dm.state_==Invest_Character_State_Machine.State.STATE_PHONE)
        {
            choixBuffer();
        }
        dm.CurrentDialogue = dialogue;
        if (dm.pm.Current_Focus_Object!=null && dm.pm.Current_Focus_Object.GetComponent<Interactible>().Interrogatoire)
        {
            dm.InterrogatoireValue += choix.InterrogatoireValue;
        }
        if (dm.pm.Current_Focus_Object != null && dm.InterrogatoireValue < -3)
        {
            dm.pm.Current_Focus_Object.GetComponent<Interactible>().Interrogatoire = false;
            if (dm.InterrogatoireValue > -3)
            {
                dm.FindDialogue(Dialogue.startType.Success, out dialogue);
            }
            else
            {
                dm.FindDialogue(Dialogue.startType.Loose, out dialogue);
            }
            dm.CurrentDialogue = dialogue;
            dm.InterrogatoireValue = 0;
        }
        if (choix.pasbien)
        {
            GetComponent<Animator>().Play("Fail");
            Invoke("StartDialogue", 0.5f);
            foreach (var item in dm.currentsButtons)
            {
                item.GetComponent<ChoixButton>().delay.Reset();
            }
        }
        else
        {
            dm.StartDialogue(dialogue);
        }

       
       
    }
    void StartDialogue()
    {
        dm.StartDialogue(dialogue);
    }
    void choixBuffer()
    {
        if (dm.state_ != Invest_Character_State_Machine.State.STATE_TALK
            && dm.state_ != Invest_Character_State_Machine.State.STATE_PHONE)
        {
            return;
        }
        foreach (var item in dm.CurrentDialogue.choices)
        {
            if (item != choix)
            {
                dm.ClickedChoix.Add(item);
            }
        }
    }
}
