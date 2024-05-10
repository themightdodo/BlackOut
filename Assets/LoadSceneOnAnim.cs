using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnAnim : MonoBehaviour
{
    public string SceneToLoad;

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(SceneToLoad);
    }
}
