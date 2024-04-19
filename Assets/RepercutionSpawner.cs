using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepercutionSpawner : MonoBehaviour
{
    public List<Repercution> RepercutionsToSpawn;

    [System.Serializable]
    public class Repercution
    {
        public string name;
        public GameObject gameObject;
    }

    private void Start()
    {
        foreach (var item in RepercutionsToSpawn)
        {
            if(PlayerPrefs.GetInt(item.name)== 1)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}