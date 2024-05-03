using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimElement : MonoBehaviour
{
    bool AnimLaunched;
    Animator animator;
   public MenuAnimManager animManager;
    public MenuAnimManager.MenuStates DesiredState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animManager = MenuAnimManager.MenuAnimManager_instance;
    }
    private void Update()
    {
        if(!AnimLaunched && animManager.menuStates == DesiredState)
        {
            animator.Play("Enter");
            AnimLaunched = true;
        }
        if (AnimLaunched && animManager.menuStates != DesiredState)
        {
            animator.Play("Out");
            AnimLaunched = false;
        }
    }
}