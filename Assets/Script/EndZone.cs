using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndZone : MonoBehaviour
{
    public Sprite Image;
    [TextArea(10, 3)]
    public string Description;

    public Item_Manager.ItemType ItemToHaveInHand;
    public Sprite AltImage;
    [TextArea(10, 3)]
    public string AltDescription;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Invest_GameManager.GM_instance.playerManager.ItemInHand!=null&&
                Invest_GameManager.GM_instance.playerManager.ItemInHand.GetComponent<Item_Manager>().itemType == ItemToHaveInHand)
            {
                Invest_GameManager.GM_instance.InvestigationDone.Invoke(AltImage, AltDescription,Invest_GameManager.GM_instance.NextSceneLose);
            }
            else
            {
                Invest_GameManager.GM_instance.InvestigationDone.Invoke(Image, Description, Invest_GameManager.GM_instance.NextSceneWin);
            }
                
        }
        
    }
}
