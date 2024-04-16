using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objectifs : MonoBehaviour
{
    public TextMeshProUGUI text;
    public List<string> objectifList;
    public int currentObjectif;

    private void Start()
    {
        text.text = objectifList[0];
    }

    public void NextObjectif(int objectif)
    {
        if(currentObjectif >= objectif)
        {
            return;
        }
        currentObjectif = objectif;
        text.text =  objectifList[currentObjectif];
    }

    public void DeleteObjectif()
    {
        text.gameObject.SetActive(false);
    }
}