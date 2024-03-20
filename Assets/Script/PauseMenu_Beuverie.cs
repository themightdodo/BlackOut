using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PauseMenu_Beuverie : Beuverie_Character_StateMachine
{
    public static bool GameIsPaused = false;
    public EventSystem eventSystem;
    public GameObject PauseFirst, WinFirst, OverFirst;
    public GameObject pauseMenuUI;
    public Animator AnimPause;
    public GameObject EndMenu;
    public TextMeshProUGUI TextEndMenu;
    public GameObject Winscreen;
    public GameObject GameOverUI;
    public float winRadius;
    public LayerMask Player;
    public float Timer = 1.5f;
    bool Endbool = false;
    // Update is called once per frame

    protected override void Start()
    {
        base.Start();
        GameIsPaused = false;
        Time.timeScale = 1f;
        AnimPause = pauseMenuUI.GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        PauseCondition();


    }
    public void endMenu(Sprite image,string text)
    {

        EndMenu.SetActive(true);
        EndMenu.GetComponent<Image>().sprite = image;
        TextEndMenu.text = text;
        Endbool = true;
        Pause();
    }
    public void PauseCondition()
    {
        if (UnityEngine.Input.GetButtonDown("Cancel")&&!Endbool)
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAHH");
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
