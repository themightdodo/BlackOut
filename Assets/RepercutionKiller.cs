using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepercutionKiller : MonoBehaviour
{
    public string RepercutionToKill;

    void Start()
    {
        PlayerPrefs.SetInt(RepercutionToKill, 0);
        PlayerPrefs.Save();
    }
}
