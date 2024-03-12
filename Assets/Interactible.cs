using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public Chara_dialogue chara_Dialogue;
    public int interactCount = 0;

    private void Start()
    {
        interactCount = 0;
    }
}
