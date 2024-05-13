using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public Chara_dialogue chara_Dialogue;
    public GameObject HandVersion;
    public GameObject ItemToDestroy;
    public bool Interrogatoire;
    public bool Success;
    public bool Loose;
    public bool drown;
    public bool noChoiceRegister;
    public int interactCount = 0;

    private void Start()
    {
       
    }
}
