using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedInteractCount : MonoBehaviour
{
    public List<Interactible> interactibles;
    int sharedInteractCount;

    private void Start()
    {
        sharedInteractCount =  interactibles[0].interactCount;
    }

    private void Update()
    {
        foreach (var item in interactibles)
        {
            if(item.interactCount > sharedInteractCount)
            {
                sharedInteractCount = item.interactCount;
                
            }
            if(item.interactCount < sharedInteractCount)
            {
                item.interactCount = sharedInteractCount;
            }
        }
    }
}
