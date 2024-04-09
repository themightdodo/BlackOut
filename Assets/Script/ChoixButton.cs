using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixButton : MonoBehaviour
{
    public Dialogue dialogue;
    public Choix choix;
    DialogueManager dm;

    private void Start()
    {
        dm = Invest_GameManager.GM_instance.DialogueManager;   
    }
    public void TriggerDialogue()
    {
        if (dm.pm.Current_Focus_Object != null && !dm.pm.Current_Focus_Object.GetComponent<Interactible>().noChoiceRegister)
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
