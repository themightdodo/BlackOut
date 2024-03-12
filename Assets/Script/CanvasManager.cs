using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Invest_Character_State_Machine
{
    public GameObject BasePanel;
    public GameObject FocusPanel;
    public GameObject DialoguePanel;

    protected override void Idle_state()
    {
        base.Idle_state();
        BasePanelUI();
    }

    protected override void Walk_state()
    {
        base.Walk_state();
        BasePanelUI();
    }
    protected override void Focus()
    {
        base.Focus();
        FocusPanelUI();

    }
    protected override void Phone_state()
    {
        base.Phone_state();
        BasePanel.SetActive(false);
        FocusPanel.SetActive(false);
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
        DialoguePanel.SetActive(true);
    }
    void FocusPanelUI()
    {
        BasePanel.SetActive(false);
        FocusPanel.SetActive(true);
        DialoguePanel.SetActive(false);
    }
    void BasePanelUI()
    {
        BasePanel.SetActive(true);
        FocusPanel.SetActive(false);
        DialoguePanel.SetActive(false);
    }
}
