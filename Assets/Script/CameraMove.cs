using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : Invest_Character_State_Machine
{
    public float Sensitivity;
    float JoyX;
    float JoyY;
    private const float Y_ANGLE_MIN = -70.0f;
    private const float Y_ANGLE_MAX = 50f;
    GameObject player;
    protected override void Start()
    {
        base.Start();

        player = pm.gameObject;
    }
    protected override void Update()
    {

        

        base.Update();




            
    }
    protected override void Idle_state()
    {
        base.Idle_state();
        MoveCamera();
       
    }
    protected override void Walk_state()
    {
        base.Walk_state();
        MoveCamera();
       
    }

    void MoveCamera()
    {
        JoyX += input.RstickHorizontal.getAxis() * Sensitivity * Time.deltaTime;
        JoyY += input.RstickVertical.getAxis() * Sensitivity * Time.deltaTime;
        JoyX = Mathf.Clamp(JoyX, Y_ANGLE_MIN, Y_ANGLE_MAX);
        Quaternion rotation = Quaternion.Euler(JoyX, JoyY, 0);
        player.transform.rotation = rotation;
        CheckInteractible();
        CheckInteractible_NoDialogue();
    }
    public void CheckInteractible()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,20f, pm.Interactibles))
        {
            pm.Current_Focus_Object = hit.transform.gameObject;
            pm.Focus.Invoke();
        }
        else
        {
            pm.Current_Focus_Object = null;
        }
    }
    public void CheckInteractible_NoDialogue()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 20f, pm.Interactibles_noDialogue))
        {
            pm.Focus_NoDialogue.Invoke(LayerMask.LayerToName(hit.collider.gameObject.layer));
            Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
        }
    }
}
