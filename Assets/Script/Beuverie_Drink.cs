using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_Drink : Beuverie_Character_StateMachine
{
    public float AnimationDelay;
    public float drinkDetectionRadius;
    public LayerMask drinkLayer;
    GameObject currentDrink;
    public GameObject Hand;
    Timer Delay;
    protected override void Start()
    {
        base.Start();
        Delay = new Timer(AnimationDelay);
        pm.drinkLayer = drinkLayer;
        pm.drinkDetectionRadius = drinkDetectionRadius;
    }
    protected override void Update()
    {

        base.Update();
        if ((input.Check.PressedDown() && state_ != State.STATE_ADDICTION)||pm.Addiction_timer_done)
        {
            pm.NearDrink = Near_Drink(transform.position, out currentDrink);
        }
    }
    protected override void Walk_state()
    {
        base.Walk_state();
        if (!input.Check.PressedDown())
        {
            pm.NearDrink = false;
        }

    }
    protected override void Idle_state()
    {
        base.Idle_state();
        pm.drinked = false;
        if (!input.Check.PressedDown())
        {
            pm.NearDrink = false;
        }
        
    }
    protected override void Drink_transition()
    {
        if (input.Check.Pressed() && pm.NearDrink && !pm.TauxAlcool.ToMuch())
        {
            Delay.CurrentValue = currentDrink.GetComponent<Beuverie_Boisson>().drink.TimeToDrink;
        }
        base.Drink_transition();
    }
    protected override void Drink_state()
    {
        base.Drink_state();
        Delay.Refresh();
        if (currentDrink != null)
        {
            currentDrink.transform.position = Hand.transform.position;
            currentDrink.transform.rotation = Hand.transform.rotation;

            if (Delay.Done())
            {
                //code de récup de boisson
                Drink drink = currentDrink.GetComponent<Beuverie_Boisson>().drink;
                CreateOnDrink();
                DestroyOnDrink();

                pm.TauxAlcool.add(drink.TauxAlcoolPlus, drink.type_, drink.Quantity);
                Destroy(currentDrink);
                currentDrink = null;
                pm.drinked = true;
                pm.NearDrink = false;
                Delay.Reset();
            }
            else if (!pm.NearDrink)
            {
                pm.NearDrink = Near_Drink(transform.position, out currentDrink);
            }
        }
        else if (!pm.NearDrink)
        {
            pm.NearDrink = Near_Drink(transform.position, out currentDrink);
        }



    }
    void CreateOnDrink()
    {
        if (currentDrink.GetComponent<Beuverie_Boisson>().EventtoCreateOnDrink != null)
        {
            Instantiate(currentDrink.GetComponent<Beuverie_Boisson>().EventtoCreateOnDrink);
        }
    }
    void DestroyOnDrink()
    {
        if (currentDrink.GetComponent<Beuverie_Boisson>().ObjectToDestroyOnDrink != null)
        {
            Destroy(currentDrink.GetComponent<Beuverie_Boisson>().ObjectToDestroyOnDrink);
        }
    }
    protected override void Addiction_state()
    {
        base.Addiction_state();
        pm.NearDrink = Near_Drink(transform.position, out currentDrink);
        if (pm.NearDrink)
        {
            Delay.CurrentValue = currentDrink.GetComponent<Beuverie_Boisson>().drink.TimeToDrink;
        }
    }
    public bool Near_Drink(Vector3 position, out GameObject currentDrink)
    {
        Collider[] drink = Physics.OverlapSphere(position, drinkDetectionRadius, drinkLayer);
        if (drink.Length > 0)
        {
            currentDrink = drink[0].gameObject;
            return true;
        }
        currentDrink = null;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, drinkDetectionRadius);
    }
}
