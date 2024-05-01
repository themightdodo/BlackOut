using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_DrinkUI : MonoBehaviour
{
    public GameObject DrinkUI;
    public Vector3 Offset;
    GameObject instanciatedDrink;
    Beuverie_PlayerManager pm;


    private void Start()
    {
        pm = Beuverie_GameManager.GM_instance.playerManager;
    }
    // Update is called once per frame
    void Update()
    {
        GameObject currentDrink = null;
        if(Near_Drink(transform.position, out currentDrink)&&instanciatedDrink==null)
        {
            instanciatedDrink = Instantiate(DrinkUI, currentDrink.transform.position + Offset, DrinkUI.transform.rotation);
           
        }
        if(Near_Drink(transform.position, out currentDrink) && instanciatedDrink != null)
        {
            instanciatedDrink.transform.position = currentDrink.transform.position + Offset;
            instanciatedDrink.transform.LookAt(Beuverie_GameManager.GM_instance.camera.transform);
        }
        if(!Near_Drink(transform.position, out currentDrink) && instanciatedDrink != null)
        {
            Destroy(instanciatedDrink);
        }
    }

    public bool Near_Drink(Vector3 position, out GameObject currentDrink)
    {
        Collider[] drink = Physics.OverlapSphere(position, pm.drinkDetectionRadius, pm.drinkLayer);
        if (drink.Length > 0)
        {
            currentDrink = drink[0].gameObject;
            return true;
        }
        currentDrink = null;
        return false;
    }
}
