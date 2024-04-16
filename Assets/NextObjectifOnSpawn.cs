using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextObjectifOnSpawn : MonoBehaviour
{
    public int ObjectifID;
    // Start is called before the first frame update
    void Start()
    {
        Invest_GameManager.GM_instance.CanvasManager.objectifs.NextObjectif(ObjectifID);
    }

}
