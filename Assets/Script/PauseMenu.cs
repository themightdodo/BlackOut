using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PauseMenu : Invest_Character_State_Machine
{
    public static bool GameIsPaused = false;
    public EventSystem eventSystem;
    public GameObject PauseFirst, WinFirst, OverFirst;
    public GameObject pauseMenuUI;
    public Animator AnimPause;
    public GameObject EndMenu;
    public GameObject EndMenuPopup;
    public TextMeshProUGUI TextEndMenu;
    public GameObject Winscreen;
    public GameObject GameOverUI;
    public GameObject OeilPanel;
    public GameObject ContinueButton;
    public float winRadius;
    public LayerMask Player;
    public float Timer = 1.5f;
    bool Endbool = false;
    Timer Oeil;
    // Update is called once per frame

    protected override void Start()
    {
        base.Start();
        Oeil = new Timer(5f);
        GameIsPaused = false;
        Time.timeScale = 1f;
        AnimPause = pauseMenuUI.GetComponent<Animator>();
        gm.InvestigationDone.AddListener(endMenuAsk);
    }

    protected override void Update()
    {
        base.Update();
        Oeil.Refresh();
        if (Oeil.Done())
        {
            OeilPanel.SetActive(false);
        }
        if (state_ == State.STATE_IDLE&&!GameIsPaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (state_ == State.STATE_WALK && !GameIsPaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

    }
    public void endMenuAsk(Sprite image, string text, string nextScene, bool noPopup)
    {
        if (noPopup == true)
        {
            endMenu();
        }
        else
        {
            EndMenuPopup.SetActive(true);
        }
        
        ContinueButton.GetComponent<Button>().onClick.AddListener(() => { LoadScene(nextScene); });
        EndMenu.GetComponent<Image>().sprite = image;
        TextEndMenu.text = text;
        Pause();
        pauseMenuUI.SetActive(false);
    }
    public void NoEndMenu()
    {
        EndMenuPopup.SetActive(false);
        Resume();
    }
    public void endMenu()
    {
        EndMenuPopup.SetActive(false);
        EndMenu.SetActive(true);
        Endbool = true;
        Pause();
    }
    protected override void Idle_state()
    {
        base.Idle_state();
        PauseCondition();
    }
    protected override void Walk_state()
    {
        base.Walk_state();
        PauseCondition();
    }
    public void PauseCondition()
    {
        if (UnityEngine.Input.GetButtonDown("Cancel")&&!Endbool)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }
    public void End(){
        Time.timeScale = 0.1f;
        
        GameOverUI.SetActive(true);
        GameOverUI.GetComponent<Animator>().Play("VraiEnter");
        
        Timer -= Time.unscaledDeltaTime;
        if (Timer <= 0)
        {
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(OverFirst);
            GameIsPaused = true;
            Time.timeScale = 0f;
            
        }
       
    }
    public void Resume()
    {
        /*AnimPause.Play("Enter");*/
        pauseMenuUI.SetActive(false);


        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(PauseFirst);
        pauseMenuUI.SetActive(true);
        /*AnimPause.Play("VraiEnter");*/
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadScene(string SceneName)
    {
        Resume();
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneName);
  
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Win()
    {

        Winscreen.SetActive(true);
        GameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(WinFirst);
        Winscreen.GetComponent<Animator>().Play("VraiEnter");
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

}
