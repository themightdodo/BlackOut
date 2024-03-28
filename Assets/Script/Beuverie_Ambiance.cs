using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_Ambiance : Beuverie_Character_StateMachine
{
    AudioManager audioManager;
    Timer Music;
    protected override void Start()
    {
        base.Start();
        audioManager = Beuverie_GameManager.GM_instance.GetComponent<AudioManager>();
        audioManager.Play("Ambiance");
        audioManager.Play("BeuverieMusic");
        Music = new Timer(204f);

    }
    protected override void Update()
    {
        base.Update();
        Music.Refresh();
        if (Music.Done())
        {
            audioManager.Play("BeuverieMusicBoucle");
        }
    }
}
