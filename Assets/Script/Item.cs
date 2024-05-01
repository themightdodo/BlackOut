using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public float ActionStartTime;
    public Timer ActionTime;
    public LayerMask InteractLayer;

    public enum ItemState
    {
        STATE_IDLE,
        STATE_ACTION,
    }
    public ItemState state_;

    public Item_Manager manager { get; set; }

    protected virtual void Start()
    {
        manager = GetComponent<Item_Manager>();
        ActionTime = new Timer(ActionStartTime);
        
        manager.Action.AddListener(ActionTransition);
    }
    protected virtual void LateUpdate()
    {
        switch (state_)
        {
            case ItemState.STATE_IDLE:
                Idle();
                break;
            case ItemState.STATE_ACTION:
                Action();
                break;
        }
    }
    protected virtual void Idle()
    {
        ActionTime.Reset();
    }
    public void ActionTransition(string layer)
    {
        InteractLayer.value  = 1 << LayerMask.NameToLayer(layer);
        state_ = ItemState.STATE_ACTION;
   
       
        
    }
    protected virtual void Action()
    {
        ActionTime.Refresh();
        if (ActionTime.Done())
        {
            state_ = ItemState.STATE_IDLE;
        }
    }
    

}
