using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class Input 
{
    public KeyCode primary;
    public KeyCode alternate;

    

    public bool Pressed()
    {
        return UnityEngine.Input.GetKey(primary) || UnityEngine.Input.GetKey(alternate);
    }
    public bool PressedDown()
    {
        return UnityEngine.Input.GetKeyDown(primary) || UnityEngine.Input.GetKeyDown(alternate);
    }

    public bool PressedUp()
    {
        return UnityEngine.Input.GetKeyUp(primary) || UnityEngine.Input.GetKeyUp(alternate);
    }

  
}
[System.Serializable]
public class Axis
{
    public string name;
    public string alternate;
 

    public float getAxis()
    {
        return UnityEngine.Input.GetAxis(name);
    }
    public float getAxisRaw()
    {
        return UnityEngine.Input.GetAxisRaw(name);
    }
}
