using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixButton : MonoBehaviour
{
    public Dialogue dialogue;

    DialogueManager dm;

    private void Start()
    {
        dm = Invest_GameManager.GM_instance.DialogueManager;   
    }
    public void TriggerDialogue()
    {
        dm.CurrentDialogue = dialogue;
        dm.StartDialogue(dialogue);
    }
}
