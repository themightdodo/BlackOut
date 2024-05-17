using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_Manager : MonoBehaviour
{
    public UnityEvent<string> Action;
    public string PickSound;
    public string ThrowSound;
    public enum ItemType
    {
        None,
        Weapon,
        Resceptacle,
        Filled_Resceptacle,
        Person,
        Key,
        Drink,
    }
    public ItemType itemType;

    public GameObject BaseItem;

    public bool usableItem;
}
