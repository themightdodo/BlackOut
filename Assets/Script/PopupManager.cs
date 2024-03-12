using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class PopupManager : Invest_Character_State_Machine
{
    public GameObject PopupScreen;
    public TextMeshProUGUI text;
    public Image image;
    public float ShowTime;
    Timer timer;
    bool ScreenActive;

    protected override void Start()
    {
        base.Start();
        pm.AddItemToInventory.AddListener(ShowScreen);
        timer = new Timer(ShowTime);
    }
    protected override void Update()
    {
        if (ScreenActive)
        {
            timer.Refresh();
        }
        if (timer.Done())
        {
            EndScreen();
        }
    }
    void ShowScreen(IndiceItem indice)
    {
        ScreenActive = true;
        PopupScreen.SetActive(true);
        text.text = indice.name;
        image.sprite = indice.Image;
        timer.Reset();
    }
    void EndScreen()
    {
        ScreenActive = false;
        PopupScreen.SetActive(false);
    }
}
