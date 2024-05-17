using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    int numberOfSoirées = 3;
    public int SoiréeNuméro;

    private void Start()
    {
        PlayerPrefs.SetInt("SoireSave", SoiréeNuméro);
        if (PlayerPrefs.GetInt("SoireSaveFixed") < SoiréeNuméro)
        {
            PlayerPrefs.SetInt("SoireSaveFixed", SoiréeNuméro);
        }
    }
}
