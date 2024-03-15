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
        choixBuffer();
        dm.CurrentDialogue = dialogue;
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
