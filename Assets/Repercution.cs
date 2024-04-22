using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repercution : MonoBehaviour
{
    public string ReperctutionToSave;

    void Start()
    {
        PlayerPrefs.SetInt(ReperctutionToSave, 1);
        PlayerPrefs.Save();
    }

}
