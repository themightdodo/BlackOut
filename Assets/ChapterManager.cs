using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public List<GameObject> Chapters;

    private void Start()
    {
        for (int i = 0; i <= PlayerPrefs.GetInt("SoireSaveFixed"); i++)
        {
            Chapters[i].SetActive(true);
        }
    }
}
