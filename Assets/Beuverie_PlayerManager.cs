using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Beuverie_PlayerManager : MonoBehaviour
{
    
    

    public float MaxtauxAlcool;

    public Taux_Alcool TauxAlcool { get; set; }
    public bool Addiction_timer_done { get; set; }
    public bool drinked { get; set; }
    public bool MouseDestination { get; set; }
    public bool NearDrink { get; set; }

    public LayerMask drinkLayer { get; set; }
    public Vector3 MouseDestinationVec { get; set; }



    public NavMeshAgent agent { get; set; }

   

    private void Start()
    {
        TauxAlcool = new Taux_Alcool(MaxtauxAlcool);
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {

    }
    public void StopMouseControl()
    {
        MouseDestination = false;
        agent.isStopped = true;
    }

}
