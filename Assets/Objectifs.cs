using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objectifs : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject Panel;
    public List<string> objectifList;
    public int currentObjectif;

    private void Start()
    {
        text.text = objectifList[0];

    }
    private void Update()
    {
        if (objectifList.Count == 0)
        {
            DeleteObjectif();
        }
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
        Panel.SetActive(false);
    }
}