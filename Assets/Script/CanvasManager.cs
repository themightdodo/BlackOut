using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : Invest_Character_State_Machine
{
    public GameObject BasePanel;
    public GameObject FocusPanel;
    public GameObject FocusPanelAlt;
    public GameObject DialoguePanel;
    public GameObject ItemInfo;
    public GameObject Panelinfo;
    public GameObject PhoneIcon;
    public GameObject UsePanel;
    public GameObject NextDialogue;
    public List<TextMeshProUGUI> FocusExaminText;
    public Objectifs objectifs;
    bool focus;

    public bool thinking;

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (!pm.PhoneActive)
        {
            PhoneIcon.SetActive(false);
        }
        else
        {
            PhoneIcon.SetActive(true);
        }

        if (pm.ItemInHand != null &&pm.ItemInHand.GetComponent<Item_Manager>().usableItem)
        {
            UsePanel.SetActive(true);
        }
        else
        {
            UsePanel.SetActive(false);
        }
    }
    protected override void Idle_state()
    {
        base.Idle_state();
        BasePanelUI();
        ItemUI();
        if(pm.Current_Focus_Object == null)
        {
            focus = false;
        }
    }

    protected override void Walk_state()
    {
        base.Walk_state();
        BasePanelUI();
        ItemUI();
        if (pm.Current_Focus_Object == null)
        {
            focus = false;
        }
    }
    protected override void Focus()
    {
        base.Focus();
        FocusPanelUI();
        focus = true;

    }
    protected override void Phone_state()
    {
        base.Phone_state();
        BasePanel.SetActive(false);
        FocusPanel.SetActive(false);
        FocusPanelAlt.SetActive(false);
        ItemInfo.SetActive(false);
        Panelinfo.SetActive(true);
    }
    protected override void Examin_state()
    {
        base.Examin_state();
        DialoguePanelUI();

    }
    protected override void Talk_state()
    {
        base.Talk_state();
        DialoguePanelUI();
  
    }
    void DialoguePanelUI()
    {
        BasePanel.SetActive(false);
        FocusPanel.SetActive(false);
        FocusPanelAlt.SetActive(false);
        DialoguePanel.SetActive(true);
        ItemInfo.SetActive(false);
        Panelinfo.SetActive(false);
    }
    void FocusPanelUI()
    {
        BasePanel.SetActive(false);
        if (pm.Current_Focus_Object.CompareTag("Interactible"))
        {
            Debug.Log("ZADZONDQOZNID%QIJZDQZ");
            FocusPanel.SetActive(true);
            FocusPanelAlt.SetActive(false);
        }
        else
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAs%QIJZDQZ");
            FocusPanelAlt.SetActive(true);
            FocusPanel.SetActive(false);
        }
        if (!thinking)
        {
            DialoguePanel.SetActive(false);
        }
        ItemInfo.SetActive(false);
        Panelinfo.SetActive(false);
    }
    void BasePanelUI()
    {
        BasePanel.SetActive(true);
        if (!focus)
        {
            FocusPanel.SetActive(false);
            FocusPanelAlt.SetActive(false);
        }
        
        if (!thinking)
        {
             DialoguePanel.SetActive(false);
        }
       
        Panelinfo.SetActive(false);
    }
    void ItemUI()
    {
        if(pm.ItemInHand != null)
        {
            ItemInfo.SetActive(true);
        }
        else
        {
            ItemInfo.SetActive(false);
        }
    }


}
