using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invest_Hand : Invest_Character_State_Machine
{
    Item_Manager item;
    public GameObject Hand;
    public Vector3 ThrowPos;
    AudioManager audioManager;


    protected override void Start()
    {
        base.Start();
        audioManager = Invest_GameManager.GM_instance.GetComponent<AudioManager>();
        pm.AddItemToHand.AddListener(PickItem);
        pm.Focus_NoDialogue.AddListener(PlayAction);
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
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

    protected override void Pick()
    {
        base.Pick();
        PickItem(pm.Current_Focus_Object.GetComponent<Interactible>().HandVersion);
        if (pm.Current_Focus_Object.GetComponent<Interactible>().ItemToDestroy)
        {
            Destroy(pm.Current_Focus_Object.GetComponent<Interactible>().ItemToDestroy);
        }
        else
        {
            Destroy(pm.Current_Focus_Object);
        }
        
    }

    void PlayAction(string layer)
    {
        Debug.Log(item);
        if (input.Check.PressedDown()&&item!=null)
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
        audioManager.Play(item.GetComponent<Item_Manager>().PickSound);
        pm.ItemInHand = item.gameObject;
    }
    
    public void throwItem()
    {
        if(input.Talk.PressedDown() && pm.Current_Focus_Object == null && item != null)
        {
            Instantiate(item.GetComponent<Item_Manager>().BaseItem,transform.position + transform.rotation * ThrowPos, transform.rotation);
            audioManager.Play(item.GetComponent<Item_Manager>().ThrowSound);
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
