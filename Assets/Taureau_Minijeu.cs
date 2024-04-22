using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Taureau_Minijeu : MonoBehaviour
{
    public float TimeBtwAttacks;
    public float QTEradius;
    public float DeathRadius;
    public string nextScene;
    public GameObject GaucheUI;
    public GameObject DroiteUI;
    public int DashSpeed;

    Timer Attack;

    public int Speed;

    GameObject Player;

    Invest_GameManager gameManager;
    InputManager inputManager;
    Invest_PlayerManager playerManager;
    CameraMove camera;
    int LeftOrRight;
    bool MinijeuxInvoke;
    public bool BoutonPressed;
    Timer ButtonPressedTime;

    private void Start()
    {
        Attack = new Timer(TimeBtwAttacks);
        gameManager = Invest_GameManager.GM_instance;
        inputManager = gameManager.GetComponent<InputManager>();
        playerManager = gameManager.playerManager;
        camera = playerManager.Camera.GetComponent<CameraMove>();
        Player = gameManager.playerManager.gameObject;
        ButtonPressedTime = new Timer(0.1f);
    }

    private void Update()
    {
        transform.LookAt(Player.transform);
      

        Attack.Refresh();
        if (Attack.Done())
        {
            Charge();
           
        }
        else
        {
            LeftOrRight = Random.Range(-1, 1);
        }
        if ((inputManager.Lstick.x <= -0.8f || inputManager.Lstick.x >= 0.8f)&&!ButtonPressedTime.Done())
        {
            BoutonPressed = true;
            ButtonPressedTime.Refresh();
        }

        if (ButtonPressedTime.Done())
        {
            BoutonPressed = false;
        }
        if (inputManager.Lstick.x >= -0.8f && inputManager.Lstick.x <= 0.8f){
            ButtonPressedTime.Reset();
            BoutonPressed = false;
        }


    }

    void CameraBehaviour()
    {
        camera.MoveCamera();

        camera.transform.LookAt(transform.position);
       


    }
    void Charge()
    {
        if (!MinijeuxInvoke)
        {
            playerManager.MiniJeu.Invoke();
        
            MinijeuxInvoke = true;
        }

        CameraBehaviour();

        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);

        if(Vector3.Distance(transform.position,Player.transform.position) < QTEradius)
        {
            QTE();
        }
    }

    void QTE()
    {
        
        switch (LeftOrRight)
        {
            case (0):
                GaucheUI.SetActive(true);
                if (inputManager.Lstick.x <= -0.8f&&BoutonPressed)
                {
                    Attack.Reset();
                    playerManager.FinMiniJeu.Invoke();
                   
                    GaucheUI.SetActive(false);
                    playerManager.FinMiniJeu.Invoke();
                    MinijeuxInvoke = false;
                    Player.transform.position -= Player.transform.right * DashSpeed;
                    
                }
                
                break;

            case (-1):
                DroiteUI.SetActive(true);
                if (inputManager.Lstick.x >= 0.8f && BoutonPressed)
                {
                    Attack.Reset();
                    playerManager.FinMiniJeu.Invoke();
                    
                    DroiteUI.SetActive(false);
                    playerManager.FinMiniJeu.Invoke();
                    MinijeuxInvoke = false;
                    Player.transform.position += Player.transform.right * DashSpeed;
               
                }

                break;
        }
        if(Vector3.Distance(transform.position, Player.transform.position) < DeathRadius)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, QTEradius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DeathRadius);
    }
}
