using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRemain : MonoBehaviour
{
    public int LifeToRemain;
    void Start()
    {
        PlayerPrefs.SetInt("LifeRemaining", LifeToRemain);
    }

}
