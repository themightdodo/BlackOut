using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveLife : MonoBehaviour
{
    public List<GameObject> Lifes;
    public string NextScene;
    // Start is called before the first frame update
    void Start()
    {
        int currentlife = PlayerPrefs.GetInt("LifeRemaining");
        if(currentlife < Lifes.Count)
        {
            Destroy(Lifes[0]);
            Lifes.RemoveAt(0);
        }
        Invoke("Remove", 1f);
    }

    void Remove()
    {
        PlayerPrefs.SetInt("LifeRemaining", PlayerPrefs.GetInt("LifeRemaining") - 1);
        Lifes[0].GetComponent<Animator>().Play("Tombe");
        if(PlayerPrefs.GetInt("LifeRemaining") == 0)
        {
            NextScene = "GameOver";
        }
        Invoke("Scene", 3f);

    }
    void Scene()
    {
        SceneManager.LoadSceneAsync(NextScene);
    }
}
