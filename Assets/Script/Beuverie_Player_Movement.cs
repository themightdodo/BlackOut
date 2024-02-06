using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_Player_Movement : Beuverie_Character_StateMachine
{
    public float StartSpeed;
    public float DrunkedSpeed;
    public float MaxSpeed;
    float currentSpeed;
    Rigidbody rb;

    
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        currentSpeed = StartSpeed;
        rb.maxLinearVelocity = MaxSpeed;
        
    }
    protected override void Idle_state()
    {
        base.Idle_state();
    }
    protected override void Walk_state()
    {
        base.Walk_state();

        if (pm.TauxAlcool.Mid())
        {
            currentSpeed = DrunkedSpeed;
            rb.AddForce(input.Lstick * currentSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = input.Lstick * currentSpeed;
        }
    }
    protected override void FollowClick_state()
    {
        pm.agent.SetDestination(pm.MouseDestinationVec);
        base.FollowClick_state();
        
    }
}
