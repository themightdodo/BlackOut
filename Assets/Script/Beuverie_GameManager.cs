using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beuverie_GameManager : MonoBehaviour
{

    public static Beuverie_GameManager GM_instance { get; private set; }
    public Beuverie_PlayerManager playerManager;
    public Camera camera;
    public string NextScenebleu;
    public string NextSceneVert;
    public string NextSceneRouge;
    public Character PlayerInfo;
    public float TimeBtwDialogues;

    private void Awake()
    {
        GM_instance = this;
    }

    public void LoadNextScene()
    {
        if(playerManager.TauxAlcool.Boisson_bleu > playerManager.TauxAlcool.Boisson_Jaune &&
            playerManager.TauxAlcool.Boisson_bleu > playerManager.TauxAlcool.Boisson_Rouge)
        {
            SceneManager.LoadScene(NextScenebleu);
        }
        if (playerManager.TauxAlcool.Boisson_Jaune > playerManager.TauxAlcool.Boisson_bleu &&
            playerManager.TauxAlcool.Boisson_Jaune > playerManager.TauxAlcool.Boisson_Rouge)
        {
            SceneManager.LoadScene(NextSceneVert);
        }
        if (playerManager.TauxAlcool.Boisson_Rouge > playerManager.TauxAlcool.Boisson_Jaune &&
        playerManager.TauxAlcool.Boisson_Rouge > playerManager.TauxAlcool.Boisson_bleu)
        {
            SceneManager.LoadScene(NextSceneRouge);
        }
        
    }
}
