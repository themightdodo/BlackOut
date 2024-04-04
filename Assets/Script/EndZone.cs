using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndZone : MonoBehaviour
{
    public Sprite Image;
    [TextArea(10, 3)]
    public string Description;

    public bool OnlyWin;
    public bool OnlyLoose;

    public Item_Manager.ItemType ItemToHaveInHand;
    public bool noPopup;
    public Sprite AltImage;
    [TextArea(10, 3)]
    public string AltDescription;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!OnlyWin && !OnlyLoose)
            {
                if (Invest_GameManager.GM_instance.playerManager.ItemInHand != null &&
                 Invest_GameManager.GM_instance.playerManager.ItemInHand.GetComponent<Item_Manager>().itemType == ItemToHaveInHand)
                {
                    Invest_GameManager.GM_instance.InvestigationDone.Invoke(AltImage, AltDescription, Invest_GameManager.GM_instance.NextSceneWin, noPopup);
                }
                else
                {
                    Invest_GameManager.GM_instance.InvestigationDone.Invoke(Image, Description, Invest_GameManager.GM_instance.NextSceneLose, noPopup);
                }
            }
            else if (OnlyWin)
            {
                if (Invest_GameManager.GM_instance.playerManager.ItemInHand != null &&
                Invest_GameManager.GM_instance.playerManager.ItemInHand.GetComponent<Item_Manager>().itemType == ItemToHaveInHand)
                {
                    Invest_GameManager.GM_instance.InvestigationDone.Invoke(AltImage, AltDescription, Invest_GameManager.GM_instance.NextSceneWin, noPopup);
                }
                else
                {
                    Invest_GameManager.GM_instance.InvestigationDone.Invoke(Image, Description, Invest_GameManager.GM_instance.NextSceneWin, noPopup);
                }
                
            }
            else if (OnlyLoose)
            {
                if (Invest_GameManager.GM_instance.playerManager.ItemInHand != null &&
                Invest_GameManager.GM_instance.playerManager.ItemInHand.GetComponent<Item_Manager>().itemType == ItemToHaveInHand)
                {
                    Invest_GameManager.GM_instance.InvestigationDone.Invoke(AltImage, AltDescription, Invest_GameManager.GM_instance.NextSceneLose, noPopup);
                }
                else
                {
                    Invest_GameManager.GM_instance.InvestigationDone.Invoke(Image, Description, Invest_GameManager.GM_instance.NextSceneLose, noPopup);
                }
               
            }
                
        }
        
    }
}
