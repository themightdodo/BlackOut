using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invest_Hand : Invest_Character_State_Machine
{
    Item_Manager item;
    public GameObject Hand;


    protected override void Start()
    {
        base.Start();
        pm.AddItemToHand.AddListener(PickItem);
        pm.Focus_NoDialogue.AddListener(PlayAction);
    }

    protected override void Idle_state()
    {
        base.Idle_state();

    }
    protected override void Walk_state()
    {
        base.Walk_state();

    }

    void PlayAction(string layer)
    {
       
        if (input.Check.PressedDown()&&item!=null)
        {
           
            item.Action.Invoke(layer);
          
        }
    }

    public void PickItem(GameObject itemPicked)
    {
        item = Instantiate(itemPicked, Hand.transform).GetComponent<Item_Manager>();

        pm.ItemInHand = item.gameObject;
    }
}
