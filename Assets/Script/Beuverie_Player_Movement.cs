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
        Rotate(gameObject, input.Lstick);
        rb.velocity = input.Lstick * currentSpeed;
/*        if (pm.TauxAlcool.Mid())
        {
            currentSpeed = DrunkedSpeed;
            rb.AddForce(input.Lstick * currentSpeed * Time.deltaTime);
        }
        else
        {
            
        }*/
    }
    protected override void FollowClick_state()
    {
        pm.agent.SetDestination(pm.MouseDestinationVec);
        base.FollowClick_state();
        
    }

    public void Rotate(GameObject gameObject, Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(gameObject.transform.forward, direction) * gameObject.transform.rotation;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation, 1000 * Time.deltaTime);

       gameObject.transform.rotation = new Quaternion(0, gameObject.transform.rotation.y, 0, gameObject.transform.rotation.w);
/*        Quaternion targetUp = Quaternion.FromToRotation(gameObject.transform.up, Vector3.up) * gameObject.transform.rotation;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetUp, 1000 * Time.deltaTime);*/
    }
}
