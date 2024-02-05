using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_GameManager : MonoBehaviour
{

    public static Beuverie_GameManager GM_instance { get; private set; }
    public Beuverie_PlayerManager playerManager;
    public Camera camera;

    private void Awake()
    {
        GM_instance = this;
    }
}
