using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatInventory : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AddToBoat")&& !Invest_GameManager.GM_instance.GetComponent<BoatComponents>().gameObjectsOnBoat.Contains(other.gameObject.GetComponent<Interactible>().HandVersion.GetComponent<Item_Manager>().BaseItem))
        {
            Debug.Log("AddToboat");
            Invest_GameManager.GM_instance.GetComponent<BoatComponents>().gameObjectsOnBoat.Add(other.gameObject.GetComponent<Interactible>().HandVersion.GetComponent<Item_Manager>().BaseItem);
        }
        if(other.CompareTag("AddToBoat")&& !Invest_GameManager.GM_instance.GetComponent<BoatComponents>().InstantiatedObjects.Contains(other.gameObject))
        {
            Invest_GameManager.GM_instance.GetComponent<BoatComponents>().InstantiatedObjects.Add(other.gameObject);
        }
    }
}
