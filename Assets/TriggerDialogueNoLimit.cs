using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueNoLimit : MonoBehaviour
{
    int triggerCount;
    private void OnTriggerEnter(Collider other)
    {
        if (triggerCount > 0)
        {
            triggerCount = 0;
        }
        if (other.CompareTag("Player"))
        {
            Invest_GameManager.GM_instance.DialogueManager.ActiveDialogue = null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (triggerCount > 0)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            Invest_GameManager.GM_instance.playerManager.Current_Focus_Object = this.gameObject;
            Invest_GameManager.GM_instance.playerManager.TriggerDialogue.Invoke();

            triggerCount++;
        }
    }
}
