using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[System.Serializable]
public class Dialogue : Node
{
    [Input]
    public Choix choix;

    public enum startType
    {
        none,
        Examin,
        Talk,
        Inventory_Show,
    }
    public startType startType_;

    public Character PersonTalking;

    public Item_Manager.ItemType ItemToHaveInHand;
    [Output(dynamicPortList = true)]
    public List<Choix> choixHand;

    [TextArea(5,20)]
    public List<string> sentences;

    [Output(dynamicPortList = true)]
    public List<Indice> indicesToShow;

    public List<Indice> indicesGiven;

    public GameObject GiveItem;
    public bool DestroyWhenGive;

    [Output(dynamicPortList = true)]
    public List<Choix> choices; 
}
