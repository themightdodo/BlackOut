using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_OeilAnim : Beuverie_Character_StateMachine
{
    public GameObject Oeil;
    protected override void Blackout_state()
    {
        base.Blackout_state();
        Oeil.GetComponent<Animator>().Play("OeilFermez");
    }
}
