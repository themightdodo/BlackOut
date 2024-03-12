using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndZone : MonoBehaviour
{
    public Sprite Image;
    [TextArea(10, 3)]
    public string Description;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invest_GameManager.GM_instance.InvestigationDone.Invoke(Image,Description);
        }
        
    }
}
