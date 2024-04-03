using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_element : MonoBehaviour
{
    public IndiceItem infos {get; set;}
    
    public void ShowInfo()
    {
        Debug.Log("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
        Invest_GameManager.GM_instance.PhoneManager.ArchiveDesc.text = infos.Desc;
        Invest_GameManager.GM_instance.PhoneManager.ArchiveImage.sprite = infos.Image;
        Invest_GameManager.GM_instance.PhoneManager.ItemDetail_transition();
    }
}
