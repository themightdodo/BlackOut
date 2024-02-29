using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Receptacle : Item
{
    public bool HaveWater;
    Invest_GameManager gm;
    Invest_PlayerManager pm;
    public LayerMask Liquids;

    protected override void Start()
    {
        base.Start();
        gm = Invest_GameManager.GM_instance;
        pm = gm.playerManager;
    }



    protected override void Action()
    {
        base.Action();
        Debug.Log(InteractLayer);
        if(InteractLayer == Liquids)
        {
            Debug.Log("takeWater");
            HaveWater = true;
            manager.itemType = Item_Manager.ItemType.Filled_Resceptacle;
        }
        
    }
}
