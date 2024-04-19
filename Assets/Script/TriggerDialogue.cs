using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDialogue : MonoBehaviour
{
    int triggerCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invest_GameManager.GM_instance.DialogueManager.ActiveDialogue = null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
       
        if(triggerCount > 0)
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
