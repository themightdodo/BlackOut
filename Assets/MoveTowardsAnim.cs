using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveTowards))]
public class MoveTowardsAnim : MonoBehaviour
{
    public Animator animator;
    MoveTowards moveTowards;

    private void Start()
    {
        moveTowards = GetComponent<MoveTowards>();    
    }

    private void Update()
    {
        if(transform.position != moveTowards.PointB)
        {
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Idle");
        }
    }
}
