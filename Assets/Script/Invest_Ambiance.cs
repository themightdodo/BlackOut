using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invest_Ambiance : MonoBehaviour
{
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = Invest_GameManager.GM_instance.GetComponent<AudioManager>();
        audioManager.Play("MusicIntrigue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
