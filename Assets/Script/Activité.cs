using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activité : MonoBehaviour
{
    public Activity activity;
    public Timer currentValue;
    public Gradient gradient;

    private void Start()
    {
        currentValue = new Timer(activity.AddictionValue);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().material.SetColor("_Color", gradient.Evaluate(1));
        }
    }
    
    public void desaturate(float Time)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Material mat = transform.GetChild(i).GetComponent<MeshRenderer>().material;

            mat.SetColor("_Color", gradient.Evaluate(Time));
        }
    }
}
