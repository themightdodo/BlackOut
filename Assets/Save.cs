using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    int numberOfSoir�es = 3;
    public int Soir�eNum�ro;

    private void Start()
    {
        PlayerPrefs.SetInt("SoireSave", Soir�eNum�ro);
        if (PlayerPrefs.GetInt("SoireSaveFixed") < Soir�eNum�ro)
        {
            PlayerPrefs.SetInt("SoireSaveFixed", Soir�eNum�ro);
        }
    }
}
