using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    Timer Drown;
    public float drownIntensity;
    GameObject CurrentDrown;
    private void Start()
    {
        Drown = new Timer(7);
    }
    private void Update()
    {
        if(CurrentDrown == null)
        {
            Drown.Reset();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Interactible>() != null&& other.GetComponent<Interactible>().drown)
        {
            Drown.Refresh();
            CurrentDrown = other.gameObject;
            other.gameObject.transform.position -= new Vector3(0, drownIntensity, 0) * Time.deltaTime;
            if (Drown.Done())
            {
                Drown.Reset();
                Destroy(other.gameObject);
            }
           
        }
    }
    
}
