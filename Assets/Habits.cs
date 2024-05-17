using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habits : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public GameObject EventToCreate;

    public void Add(GameObject gameObject)
    {
        if (!gameObjects.Contains(gameObject)){
            gameObjects.Add(gameObject);
            if(gameObjects.Count == 4)
            {
                Instantiate(EventToCreate);
                GetComponent<Invest_PlayerManager>().PhoneActive = true;
            }
          
        }       
    }
}
