using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteractible : MonoBehaviour
{
     int InteractCount = -2;
    public bool Success;
    PhoneManager phoneManager;

    private void Start()
    {
        phoneManager = Invest_GameManager.GM_instance.PhoneManager;
    }

    public void Interact(Chara_dialogue chara_Dialogue)
    {
        InteractCount++;
        phoneManager.Calling_transition(chara_Dialogue, InteractCount,gameObject);
    }
}
