using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[System.Serializable]
public class Dialogue : Node
{
    [Input]
    public Choix choix;

    public bool SuccessPoint;

    public bool LoosePoint;

    public bool BadDialogue;

    public enum startType
    {
        none,
        Examin,
        Talk,
        Talk2,
        Success,     
        Inventory_Show,
        Answer,
        Interrogatoire,
        Loose,
    }
    public startType startType_;

    public Character PersonTalking;
    public string AnimToPlay;
    public float timeBtwLetter = 0.022f * 2;

    public Item_Manager.ItemType ItemToHaveInHand;
    [Output(dynamicPortList = true)]
    public List<Choix> choixHand;

    public IndiceItem IndiceToHave;
    public IndiceItem IndiceToHave2;
    [Output(dynamicPortList = true)]
    public List<Choix> choixIndice;

    [TextArea(5,20)]
    public List<string> sentences;

    [Output(dynamicPortList = true)]
    public List<Indice> indicesToShow;

    public List<Indice> indicesGiven;

    public GameObject GiveItem;
    public bool DestroyWhenGive;

    public bool unlimitedEvent;
    public GameObject EventToCreate;

    [Output(dynamicPortList = true)]
    public List<Choix> choices;

  
}
