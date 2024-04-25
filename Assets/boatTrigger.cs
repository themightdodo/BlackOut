using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invest_GameManager.GM_instance.GetComponent<BoatComponents>().Active();
        
            Destroy(gameObject);
        }

    }

}
