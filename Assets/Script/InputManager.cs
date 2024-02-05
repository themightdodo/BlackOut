using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float JoystickDeadzone;

    [Header("INPUTS")]

    public Input Inventory;
    public Input Check;
    public Input Talk;
    public Input Cancel;
    public Input Phone;

    [Header("AXIS")]


    public Axis RstickHorizontal;
    public Axis RstickVertical;
    public Axis LstickHorizontal;
    public Axis LstickVertical;

    [Header("VECTOR")]

    public Vector3 Rstick;
    public Vector3 Lstick;

    private void Update()
    {

        var Rstick_untrad = new Vector3(RstickHorizontal.getAxis(),0, RstickVertical.getAxis()).normalized;
        var Lstick_untrad = new Vector3(LstickHorizontal.getAxis(),0, LstickVertical.getAxis()).normalized;

        Lstick = Lstick_untrad;
        Rstick = Rstick_untrad;
    }
}
