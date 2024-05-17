using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimManager : MonoBehaviour
{
    public static MenuAnimManager MenuAnimManager_instance { get; private set; }
    public bool ingame;
    [System.Serializable]
    public enum MenuStates
    {
        SETTINGS,
        HOME,
        GAME,
        PAUSE,
        CHAPTERS,
    }

    public MenuStates menuStates;



    private void Awake()
    {
        MenuAnimManager_instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (!ingame)
        {
            ChangeState(MenuStates.HOME);
        }
        
    }

    public void ChangeState(MenuStates desiredState)
    {
        menuStates = desiredState;
    }

    private void Update()
    {
        if (UnityEngine.Input.GetButtonDown("Cancel")&&(menuStates ==  MenuStates.SETTINGS|| menuStates == MenuStates.CHAPTERS))
        {
            if (ingame)
            {
                ChangeState(MenuStates.GAME);
            }
            else
            {
                ChangeState(MenuStates.HOME);
            }
            return;
        }
        if (UnityEngine.Input.GetButtonDown("Cancel") && (menuStates == MenuStates.GAME))
        {
            ChangeState(MenuStates.HOME);
            return;
        }
        if (UnityEngine.Input.GetButtonDown("Cancel") && (menuStates == MenuStates.HOME)&&ingame)
        {
            ChangeState(MenuStates.GAME);
            return;
        }
    }

    public void GoBack()
    {
        if (UnityEngine.Input.GetButtonDown("Cancel") && (menuStates == MenuStates.SETTINGS || menuStates == MenuStates.CHAPTERS))
        {
            if (ingame)
            {
                ChangeState(MenuStates.GAME);
            }
            else
            {
                ChangeState(MenuStates.HOME);
            }
            return;
        }
        if (UnityEngine.Input.GetButtonDown("Cancel") && (menuStates == MenuStates.GAME))
        {
            ChangeState(MenuStates.HOME);
            return;
        }
        if (UnityEngine.Input.GetButtonDown("Cancel") && (menuStates == MenuStates.HOME))
        {
            ChangeState(MenuStates.GAME);
            return;
        }
    }
}
