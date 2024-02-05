using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addiction : Beuverie_Character_StateMachine
{
    public float AddictionStartTime;
    public float AddictionDrinkDetectionRadius;
    public Timer Addiction_timer { get; set; }

    protected override void Start()
    {
        base.Start();
        Addiction_timer = new Timer(AddictionStartTime);
        pm.Addiction_timer_done = Addiction_timer.Done();
    }
    protected override void Update()
    {
        base.Update();
        Addiction_timer.Refresh();
        pm.Addiction_timer_done = Addiction_timer.Done();

    }
    protected override void Addiction_state()
    {
        base.Addiction_state();
        GameObject Drink;
        GetNearestDrink(out Drink);
        if(Drink == null)
        {
            Debug.Log("plus de verres :(");
            return;
        }
        pm.agent.SetDestination(Drink.transform.position);


    }
    protected override void Drink_state()
    {
        Addiction_timer.Reset();
        pm.agent.isStopped = true;
        base.Drink_state();
        
    }

    void GetNearestDrink(out GameObject Drink)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, AddictionDrinkDetectionRadius, pm.drinkLayer);
        if (colliders.Length == 0)
        {
            Drink = null;
            return;
        }

        float MinDist = Vector3.Distance(transform.position, colliders[0].gameObject.transform.position);
        GameObject nearestDrink = colliders[0].gameObject;

        foreach (var item in colliders)
        {
            float itemDist = Vector3.Distance(transform.position, item.transform.position);
            if(MinDist> itemDist)
            {
                MinDist = itemDist;
                nearestDrink = item.gameObject;
            }
        }
        Drink = nearestDrink;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AddictionDrinkDetectionRadius);
    }
}
