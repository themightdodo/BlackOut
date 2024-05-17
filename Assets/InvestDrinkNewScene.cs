using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvestDrinkNewScene : Item
{
    public string SceneToLoad;
    bool sceneLoaded;
    Invest_PlayerManager pm;

    protected override void Start()
    {
        base.Start();
        pm = Invest_GameManager.GM_instance.playerManager;
        pm.GetComponent<CharacterController>().enabled = false;
    }

    private void OnDestroy()
    {
        pm.GetComponent<CharacterController>().enabled = true;
    }

    protected override void Action()
    {
        base.Action();
        if (sceneLoaded)
        {
            return;
        }
        SceneManager.LoadSceneAsync(SceneToLoad);
        sceneLoaded = true;
    }
}
