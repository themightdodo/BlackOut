using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnim : Item
{
    public Animator animator;


    protected override void Idle()
    {
        base.Idle();
        animator.Play("Idle");
    }

    protected override void Action()
    {
        base.Action();

        animator.Play("Action");
    }
}
