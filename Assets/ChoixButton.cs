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
        foreach (var item in dm.CurrentDialogue.choices)
        {
            if (item != choix)
            {
                dm.ClickedChoix.Add(item);
            }          
        }
        dm.CurrentDialogue = dialogue;
        dm.StartDialogue(dialogue);
     
    }
}
