using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_Anim : Beuverie_Character_StateMachine
{
    public Animator anim;

    protected override void Idle_state()
    {
        base.Idle_state();
        anim.Play("Idle");
    }
    protected override void Walk_state()
    {
        base.Walk_state();
        anim.Play("Walk");
    }
    protected override void Addiction_state()
    {
        base.Addiction_state();
        anim.Play("Walk");
    }
    protected override void Drink_state()
    {
        base.Drink_state();
        anim.Play("Drink");
    }
    protected override void FollowClick_state()
    {
        base.FollowClick_state();
        anim.Play("Walk");
    }
}
