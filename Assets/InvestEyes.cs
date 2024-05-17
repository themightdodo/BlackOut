using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestEyes : MonoBehaviour
{
    public List<GameObject> Eyes;
    public float TimeBtwInstance;

    public void InstanciateEyes()
    {
        StartCoroutine(InstanceEyes());
    }

    IEnumerator InstanceEyes()
    {
        foreach (var item in Eyes)
        {
            item.SetActive(true);

            yield return new WaitForSeconds(TimeBtwInstance);
        }
        
    }
}
