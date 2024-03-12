using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_Manager : MonoBehaviour
{
    public UnityEvent<string> Action;
    public enum ItemType
    {
        None,
        Weapon,
        Resceptacle,
        Filled_Resceptacle,
    }
    public ItemType itemType;
}
