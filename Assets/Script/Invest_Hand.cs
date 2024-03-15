using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invest_Hand : Invest_Character_State_Machine
{
    Item_Manager item;
    public GameObject Hand;
    public Vector3 ThrowPos;


    protected override void Start()
    {
        base.Start();
        pm.AddItemToHand.AddListener(PickItem);
        pm.Focus_NoDialogue.AddListener(PlayAction);
    }
    protected override void Update()
    {
        base.Update();
        pm.throwingItem.Refresh();
    }
    protected override void Idle_state()
    {
        base.Idle_state();
        throwItem();
    }
    protected override void Walk_state()
    {
        base.Walk_state();
        throwItem();
    }

    void PlayAction(string layer)
    {
       
        if (input.Talk.PressedDown()&&item!=null)
        {
           
            item.Action.Invoke(layer);
          
        }
    }

    public void PickItem(GameObject itemPicked)
    {
        if(item != null)
        {
            Instantiate(item.GetComponent<Item_Manager>().BaseItem, transform.position + transform.rotation * ThrowPos, transform.rotation);
            Destroy(item.gameObject);
            item = null;
            pm.ItemInHand = null;
            pm.throwingItem.Reset();
           
        }
        item = Instantiate(itemPicked, Hand.transform).GetComponent<Item_Manager>();

        pm.ItemInHand = item.gameObject;
    }
    
    public void throwItem()
    {
        if(input.Check.PressedDown() && pm.Current_Focus_Object == null && item != null)
        {
            Instantiate(item.GetComponent<Item_Manager>().BaseItem,transform.position + transform.rotation * ThrowPos, transform.rotation);
            Destroy(item.gameObject);
            item = null;
            pm.ItemInHand = null;
            pm.throwingItem.Reset();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * ThrowPos, 1f);
    }
}
