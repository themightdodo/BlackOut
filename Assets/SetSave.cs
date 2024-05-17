using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSave : MonoBehaviour
{
    public List<string> SoiréesName;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SoireSave"))
        {
            PlayerPrefs.SetInt("SoireSave", 0);
        }
        if (!PlayerPrefs.HasKey("SoireSaveFixed"))
        {
            PlayerPrefs.SetInt("SoireSaveFixed", 0);
        }
        for (int i = 0; i < SoiréesName.Count; i++)
        {
            PlayerPrefs.SetString("Soire" + i, SoiréesName[i]);
        }
        
    }
}
