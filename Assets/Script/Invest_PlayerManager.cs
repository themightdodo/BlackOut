using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Invest_PlayerManager : MonoBehaviour
{
    public UnityEvent Focus;
    public UnityEvent<string> Focus_NoDialogue;
    public UnityEvent MiniJeu;
    public UnityEvent FinInteraction;
    public UnityEvent<IndiceItem> AddItemToInventory;
    public UnityEvent<GameObject> AddItemToHand;
    public Timer throwingItem;
    public Timer Interaction_cooldown;
    public GameObject Current_Focus_Object;
    public GameObject ItemInHand;
    public LayerMask Interactibles;
    public LayerMask Interactibles_noDialogue;

    private void Start()
    {
        Interaction_cooldown = new Timer(0.5f);
        throwingItem = new Timer(0.5f);
    }
    private void Update()
    {
        Interaction_cooldown.Refresh();
    }
}
