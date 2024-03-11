using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activité : MonoBehaviour
{
    public Activity activity;
    public Timer currentValue;

    private void Start()
    {
        currentValue = new Timer(activity.AddictionValue);
        
    }
}
