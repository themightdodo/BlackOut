using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public Vector3 PointA;
    public Vector3 PointB;
    public float Speed;

    private void Start()
    {
        transform.position = PointA;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, PointB,Time.deltaTime*Speed);   
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(PointA, 2f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(PointB, 2f);
    }
}
