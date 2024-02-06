using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_Drink : Beuverie_Character_StateMachine
{
    public float AnimationDelay;
    public float drinkDetectionRadius;
    public LayerMask drinkLayer;
    GameObject currentDrink;
    Timer Delay;
    protected override void Start()
    {
        base.Start();
        Delay = new Timer(AnimationDelay);
        pm.drinkLayer = drinkLayer;
    }
    protected override void Update()
    {

        base.Update();
        if (input.Check.PressedDown() && state_ != State.STATE_ADDICTION)
        {
            pm.NearDrink = Near_Drink(transform.position, out currentDrink);
        }
    }
    protected override void Idle_state()
    {
        base.Idle_state();
        pm.drinked = false;
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
        if (Delay.Done())
        {
            //code de récup de boisson
            Drink drink = currentDrink.GetComponent<Beuverie_Boisson>().drink;
            pm.TauxAlcool.add(drink.TauxAlcoolPlus,drink.type_);
            Destroy(currentDrink);
            currentDrink = null;
            pm.drinked = true;
            pm.NearDrink = false;
            Delay.Reset();
        }


    }
    protected override void Addiction_state()
    {
        base.Addiction_state();
        pm.NearDrink = Near_Drink(transform.position, out currentDrink);
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
