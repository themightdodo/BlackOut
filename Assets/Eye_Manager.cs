using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Manager : Beuverie_Character_StateMachine
{
    public GameObject ObjectToInstanciate;
    public List<Transform> Positions;
    public List<GameObject> InstanciatedObjects;

    public float number;
    float lastnumber;
    int basePositionCount;

    protected override void Start()
    {
        base.Start();
        basePositionCount = Positions.Count;
    }

    protected override void Update()
    {
        base.Update();
        if(Positions.Count == 0)
        {
            return;
        }
        Debug.Log(number);
        number = (pm.TauxAlcool.BoissonTaken() * basePositionCount) / pm.TauxAlcool.MaxBoisson;
        if (number < 1 && pm.TauxAlcool.BoissonTaken() != 0)
        {
            number = 1;
        }
        if (lastnumber < number)
        {
            lastnumber = number;

            for (int i = 0; i < number; i++)
            {
                InstanciatedObjects.Add(Instantiate(ObjectToInstanciate, Positions[i].position, Positions[i].rotation));
                Positions.RemoveAt(i);
            }
        }

    }
    protected override void Drink_transition()
    {
        if (input.Check.Pressed() && pm.NearDrink && !pm.TauxAlcool.ToMuch())
        {
            PlayAnim();
            pm.StopMouseControl();
            state_ = State.STATE_DRINK;
        }
        
    }
    protected override void Addiction_state()
    {
        base.Addiction_state();
        PlayAnim();
    }
    protected override void Drink_state()
    {
        base.Drink_state();
        PlayAnim();
    }

    void PlayAnim()
    {
        foreach (var item in InstanciatedObjects)
        {
            float randomNumber = Random.Range(0.2f, 1.5f);
            item.GetComponent<Animator>().speed = randomNumber;
            item.GetComponent<Animator>().Play("Laugh");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var item in Positions)
        {
            Gizmos.DrawSphere(item.transform.position, 1f);
        }
    }
}
