using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invest_Inventory : Invest_Character_State_Machine
{
    public List<IndiceItem> CurrentIndices;
    public List<GameObject> InventoryContainer;
    public GameObject ButtonPrefab;

    protected override void Start()
    {
        base.Start();
        pm.AddItemToInventory.AddListener(AddToInventory);
        UpdateInventoryStart();
    }
    public void UpdateInventoryStart()
    {
        for (int i = 0; i < CurrentIndices.Count; i++)
        {
            foreach (var Inventoryitem in InventoryContainer)
            {
                GameObject Button = Instantiate(ButtonPrefab, Inventoryitem.transform);
                Button.GetComponent<Image>().sprite = CurrentIndices[i].Image;
                Button.GetComponent<Inventory_element>().infos = CurrentIndices[i];
            }
        }

        
    }
    public void UpdateInventory()
    {

            foreach (var Inventoryitem in InventoryContainer)
            {
                GameObject Button = Instantiate(ButtonPrefab, Inventoryitem.transform);
                Button.GetComponent<Image>().sprite = CurrentIndices[CurrentIndices.Count-1].Image;
                Button.GetComponent<Inventory_element>().infos = CurrentIndices[CurrentIndices.Count-1];
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
