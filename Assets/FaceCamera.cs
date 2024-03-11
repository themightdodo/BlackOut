using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Beuverie_GameManager.GM_instance.camera.transform.position);
    }
}
