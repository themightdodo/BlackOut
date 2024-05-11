using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioManager>().Play("Music");
    }


}
