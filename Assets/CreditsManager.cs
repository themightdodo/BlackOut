using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    Timer timer;
    public string nextScene;
    public float TimeBeforenext;
    bool loaded;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioManager>().Play("Musique");
        timer = new Timer(TimeBeforenext);
    }
    private void Update()
    {
        if (loaded)
        {
            return;
        }
        timer.Refresh();
        if (timer.Done())
        {
            SceneManager.LoadScene(nextScene);
            loaded = true;
        }
    }

}
