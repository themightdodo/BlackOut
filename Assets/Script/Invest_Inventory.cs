using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invest_Inventory : Invest_Character_State_Machine
{
    public List<IndiceItem> CurrentIndices;
    public GameObject InventoryContainer;
    public GameObject ButtonPrefab;

    protected override void Start()
    {
        base.Start();
        pm.AddItemToInventory.AddListener(AddToInventory);
    }
    public void UpdateInventory()
    {
        foreach (var item in CurrentIndices)
        {
            GameObject Button = Instantiate(ButtonPrefab, InventoryContainer.transform);
            Button.GetComponent<Image>().sprite = item.Image;
            Button.GetComponent<Inventory_element>().infos = item;
        }
    }

    public void AddToInventory(IndiceItem indice)
    {
        if (CurrentIndices.Contains(indice))
        {
            return;
        }
        CurrentIndices.Add(indice);
        UpdateInventory();
    }

   
}
