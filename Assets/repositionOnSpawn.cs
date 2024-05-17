using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repositionOnSpawn : MonoBehaviour
{
    public Quaternion WantedRotation;
    public Vector3 WantedPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = WantedRotation;
        transform.position = WantedPosition;
    }

}
