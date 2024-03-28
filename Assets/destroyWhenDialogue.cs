using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWhenDialogue : MonoBehaviour
{
    public Chara_dialogue DialogueLinked;
    Invest_PlayerManager pm;
    Invest_GameManager gm;
    Chara_dialogue DialogueBuffer;

    private void Start()
    {
        gm = Invest_GameManager.GM_instance;
        pm = gm.playerManager;
        pm.FinInteraction.AddListener(DestroyObject);
    }
    private void Update()
    {
        if(gm.DialogueManager.state_ == Invest_Character_State_Machine.State.STATE_TALK)
        {
            DialogueBuffer = gm.DialogueManager.ActiveDialogue;
        }
    }
    void DestroyObject()
    {
       
        if (DialogueLinked == DialogueBuffer)
        {
            Destroy(gameObject);
        }
    }
}
