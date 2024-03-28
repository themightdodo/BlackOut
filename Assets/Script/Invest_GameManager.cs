using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Invest_GameManager : MonoBehaviour
{
    public static Invest_GameManager GM_instance { get; private set; }
    public Invest_PlayerManager playerManager;
    public CanvasManager CanvasManager;
    public DialogueManager DialogueManager;
    public PhoneManager PhoneManager;
    public UnityEvent<Sprite,string,string> InvestigationDone;
    public PauseMenu menuManager;

    public string NextSceneWin;
    public string NextSceneLose;

    private void Awake()
    {
        GM_instance = this;
    }
}