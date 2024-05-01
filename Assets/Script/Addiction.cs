using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Addiction : Beuverie_Character_StateMachine
{
    public float AddictionStartTime;
    public float AddictionDrinkDetectionRadius;
    public float currentAddictionTime;
    public Animator AddictionPersoAnim;
    public Timer Addiction_timer { get; set; }

    protected override void Start()
    {
        base.Start();
        Addiction_timer = new Timer(AddictionStartTime);
        pm.Addiction_timer_done = Addiction_timer.Done();
        pm.leaveActivity.AddListener(ActivityLeave);
    }
    protected override void Update()
    {
        base.Update();
        if (pm.desactivateAddictionUntilDrink)
        {
            return;
        }
        Addiction_timer.Refresh();
        Debug.Log(Addiction_timer.Done());
        pm.Addiction_timer_done = Addiction_timer.Done();
        currentAddictionTime = Addiction_timer.CurrentValue;

        if (state_!=State.STATE_DRINK)
        {
            AddictionPersoAnim.Play("Addiction",-1,1 - (Addiction_timer.CurrentValue / Addiction_timer.StartValue));
        }
        AddictionPersoAnim.speed = 1 * Addiction_timer.StartValue / 3f;
        if (pm.inActivity&&!pm.currentActivityData.currentValue.Done())
        {
            ActivityStatus();
        }
    }
    void ActivityStatus()
    {
        Debug.Log("Activity");
        Addiction_timer.CurrentValue = pm.currentActivityData.currentValue.CurrentValue;
        pm.currentActivityData.desaturate( Addiction_timer.CurrentValue/ pm.currentActivityData.currentValue.StartValue);
        AddictionPersoAnim.Play("Addiction", -1, 1 - (Addiction_timer.CurrentValue / pm.currentActivityData.currentValue.StartValue));
    }
    public void ActivityLeave()
    {
        Debug.Log("leave");
        Addiction_timer.Remove(pm.currentActivityData.currentValue.CurrentValue-Addiction_timer.StartValue);

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
        AddictionPersoAnim.Play("Idle");
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
