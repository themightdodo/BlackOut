using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public Vector3 PointA;
    public Vector3 PointB;
    public float Speed;
    public bool NoLook;

    private void Start()
    {
        transform.position = PointA;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, PointB,Time.deltaTime*Speed);
        if (NoLook)
        {
            return;
        }
        transform.LookAt(PointB);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(PointA, 2f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(PointB, 2f);
    }
}
