using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtons : MonoBehaviour
{
    

    public void PlaySound(string SoundToPlay)
    {
        Invest_GameManager.GM_instance.GetComponent<AudioManager>().Play(SoundToPlay);
    }
}
