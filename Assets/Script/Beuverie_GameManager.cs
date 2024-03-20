using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beuverie_GameManager : MonoBehaviour
{

    public static Beuverie_GameManager GM_instance { get; private set; }
    public Beuverie_PlayerManager playerManager;
    public Camera camera;
    public string NextScene;
    public Character PlayerInfo;
    public float TimeBtwDialogues;

    private void Awake()
    {
        GM_instance = this;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }
}
