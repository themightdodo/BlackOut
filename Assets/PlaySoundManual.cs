using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundManual : MonoBehaviour
{
    public string SoundToPlay;
    public AudioManager audioManager;

    private void Start()
    {
        audioManager.Play(SoundToPlay);
    }
}
