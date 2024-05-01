using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public MenuAnimManager.MenuStates stateTogoto;

    public void changeState()
    {
        MenuAnimManager.MenuAnimManager_instance.ChangeState(stateTogoto);
    }
}
