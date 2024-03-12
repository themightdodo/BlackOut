using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invest_PlayerMovement : Invest_Character_State_Machine
{
    public float Speed;
    public float gravity = 9;

    CharacterController cc;

    protected override void Start()
    {
        base.Start();

        cc = GetComponent<CharacterController>();
    }

    protected override void Idle_state()
    {
        base.Idle_state();
        cc.Move(new Vector3(0, -gravity, 0));
    }
    protected override void Walk_state()
    {
        base.Walk_state();
        Vector3 move = transform.rotation * input.Lstick * Speed;
        cc.Move(new Vector3(move.x,-gravity, move.z));
        
    }
}
