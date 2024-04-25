using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invest_PlayerMovement : Invest_Character_State_Machine
{
    public float Speed;
    public float gravity = 9;

    CharacterController cc;
    public float WaterDetection;
    public Vector3 WaterDetectPosition;
    public LayerMask WaterLayer;
    public bool Water;
    protected override void Start()
    {
        base.Start();

        cc = GetComponent<CharacterController>();
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        Water = inWater();

    }
    protected override void Idle_state()
    {
        base.Idle_state();

        
      
            cc.Move(new Vector3(0, -gravity, 0));
     

        
    }
    protected override void Walk_state()
    {
        base.Walk_state();
        Vector3 move = transform.rotation * input.Lstick * Speed * Time.deltaTime;
        if (inWater())
        {
            move = transform.rotation * input.Lstick * (Speed/2.5f) * Time.deltaTime;
        }

        

        cc.Move(new Vector3(move.x, -gravity, move.z));
        

            
    }

    public bool inWater()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + WaterDetectPosition, WaterDetection, -transform.up, out hit, WaterDetection, WaterLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + WaterDetectPosition, WaterDetection);
    }
}
