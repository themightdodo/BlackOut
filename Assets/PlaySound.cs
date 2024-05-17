using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public string SoundToPlay;


    private void OnEnable()
    {
        Invest_GameManager.GM_instance.GetComponent<AudioManager>().Play(SoundToPlay);
    }

    
}
