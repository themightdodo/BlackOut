using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_element : MonoBehaviour
{
    public IndiceItem infos {get; set;}
    
    public void ShowInfo()
    {
        Invest_GameManager.GM_instance.PhoneManager.ArchiveDesc.text = infos.Desc;
    }
}
