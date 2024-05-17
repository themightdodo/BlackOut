using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   
    Resolution[] resolutions;

   
    public  TMPro.TMP_Dropdown resolutionDropdown;
    public static MenuManager Instance { get; private set; }
    public GameObject MainMenu;
    public GameObject LancerPartie;
    public GameObject LancerTraining;
    public AudioMixer BeuverieMaster;
    public AudioMixer InvestMaster;
    string scenenamebuffer;


    // Called zero
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }

        

    }


    private void Start()
    {
       
       resolutions = Screen.resolutions;
        if(resolutionDropdown!= null)
        {
            resolutionDropdown.ClearOptions();
        }

        /// on converti la  liste de resolotion en sitrng pour pouvoir  l'ajouter � la Dropdown;
        List<string> options = new();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        if (resolutionDropdown != null)
        {
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }


        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SetResolution ( int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

  
 

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume(float Volume)
    {
        BeuverieMaster.SetFloat("BeuverieMaster", Volume);
        InvestMaster.SetFloat("InvestMaster", Volume);
    }
    public void SetSoundEffect(float Volume)
    {
        BeuverieMaster.SetFloat("SFX", Volume);
        InvestMaster.SetFloat("SFX", Volume);
    }
    public void SetMusic(float Volume)
    {
        BeuverieMaster.SetFloat("Music", Volume);
        InvestMaster.SetFloat("Music", Volume);
    }
    
    public void LoadScene(string SceneName)
    {
       
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(SceneName);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void LoadLastSoire()
    {

        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(PlayerPrefs.GetString("Soire" + PlayerPrefs.GetInt("SoireSave")));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void LoadLastSoireDelayed()
    {

        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        scenenamebuffer = PlayerPrefs.GetString("Soire" + PlayerPrefs.GetInt("SoireSave"));
        this.Invoke("LoadSceneAsync", 3f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void LoadSceneAsync()
    {

        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadSceneAsync(scenenamebuffer);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void LoadSceneDelayed(string SceneName)
    {

        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        scenenamebuffer = SceneName;
        this.Invoke( "LoadSceneAsync", 3f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }


    public void SetMenu(GameObject Menu)
    {
        Menu.SetActive(true);
    }

    public void ClearMenu(GameObject Menu)
    {
        Menu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }




    private void Update()
    {
/*        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;*/
        /*        if (GameManager.GM_instance.IsTrainingMode)
                {
                    LancerPartie.SetActive(false) ;
                    LancerTraining.SetActive(true) ;
                }else
                {
                    LancerPartie.SetActive(true) ;
                    LancerTraining.SetActive(false) ;
                }*/
    }
}
