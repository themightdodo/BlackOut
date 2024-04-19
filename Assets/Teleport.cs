using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 TpPosition;

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Invest_GameManager.GM_instance.playerManager.Current_Focus_Object = null;
            Invest_GameManager.GM_instance.DialogueManager.ActiveDialogue = null;
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = TpPosition;
            other.GetComponent<CharacterController>().enabled = true;
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(TpPosition, 1f);
    }
}
