using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fog : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")||other.CompareTag("Bateau"))
        {
            if(RenderSettings.fogDensity != 1)
            {
                RenderSettings.fogDensity += 0.001f;
            }
           
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bateau"))
        {
            RenderSettings.fogDensity = 0.004f;
        }
    }
}
