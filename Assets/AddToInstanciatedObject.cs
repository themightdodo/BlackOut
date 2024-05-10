using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToInstanciatedObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invest_GameManager.GM_instance.GetComponent<BoatComponents>().InstantiatedObjects.Add(gameObject);  
    }

}
