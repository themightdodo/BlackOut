using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Beuverie_PlayerManager : MonoBehaviour
{
    
    

    public float MaxtauxAlcool;

    public int MaxBoisson;

    public float Taux;
    public Taux_Alcool TauxAlcool { get; set; }
    public bool Addiction_timer_done { get; set; }
    public bool drinked { get; set; }

    public bool desactivateAddictionUntilDrink = false;
    public bool MouseDestination { get; set; }
    public bool NearDrink;
    public bool inActivity;

    public UnityEvent leaveActivity;
    public Activité currentActivityData { get; set; }
    public LayerMask drinkLayer { get; set; }
    public float drinkDetectionRadius;
    public LayerMask ActivityLayer;

    public Vector3 MouseDestinationVec { get; set; }


    public NavMeshAgent agent { get; set; }

   

    private void Start()
    {
        TauxAlcool = new Taux_Alcool(MaxtauxAlcool,MaxBoisson);
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        Taux = TauxAlcool.Taux;
        if (inActivity)
        {
            currentActivityData.currentValue.Refresh();
       
        }
        if (TauxAlcool.ToMuch())
        {
            Beuverie_GameManager.GM_instance.Invoke("LoadNextScene", 2f);
        }
    }
    public void StopMouseControl()
    {
        MouseDestination = false;
        agent.isStopped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.gameObject.CompareTag("Activity"))
        {
            inActivity = true;
            currentActivityData = other.gameObject.GetComponent<Activité>();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Activity"))
        {
            inActivity = false;
            leaveActivity.Invoke();
        }
    }

}
