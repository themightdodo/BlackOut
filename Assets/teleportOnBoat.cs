using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportOnBoat : MonoBehaviour
{
    public Vector3 TpPosition;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = transform.position + transform.rotation * TpPosition;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * TpPosition, 1f);
    }
}
