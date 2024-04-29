using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonActive : MonoBehaviour
{
    Timer timer;

    private void Start()
    {
        timer = new Timer(2);
    }

    public void Active()
    {
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
        timer.Reset();
    }

    private void Update()
    {
        timer.Refresh();
        if (!transform.GetChild(0).gameObject.activeSelf&&timer.Done())
        {
            Active();
        }
    }
}
