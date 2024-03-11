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
    public UnityEvent InvestigationDone;

    private void Awake()
    {
        GM_instance = this;
        InvestigationDone.AddListener(EndGame);
    }

    void EndGame()
    {
        Debug.Log("End!");
    }
}
