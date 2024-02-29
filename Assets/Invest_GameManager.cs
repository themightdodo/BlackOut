using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invest_GameManager : MonoBehaviour
{
    public static Invest_GameManager GM_instance { get; private set; }
    public Invest_PlayerManager playerManager;
    public DialogueManager DialogueManager;

    private void Awake()
    {
        GM_instance = this;
    }
}
