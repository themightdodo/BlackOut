using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnAfterDelay : MonoBehaviour
{
    public float delay;
    public GameObject Object;
    Timer timer;

    private void Start()
    {
        timer = new Timer(delay);
    }

    private void Update()
    {
        timer.Refresh();
        if (timer.Done())
        {
            Instantiate(Object);
            Destroy(gameObject);
        }
    }
}
