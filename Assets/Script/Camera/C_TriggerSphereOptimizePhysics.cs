using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TriggerSphereOptimizePhysics : MonoBehaviour
{

    public SphereCollider S_Coll;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<buoyancyObject>())
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<buoyancyObject>())
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
