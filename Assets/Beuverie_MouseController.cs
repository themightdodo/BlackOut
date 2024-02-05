using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beuverie_MouseController : MonoBehaviour
{
    Camera cam;
  
    Beuverie_PlayerManager pm;
    void Start()
    {
        cam = Beuverie_GameManager.GM_instance.camera;
        pm = Beuverie_GameManager.GM_instance.playerManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
            RaycastHit hit;
            pm.MouseDestination = Physics.Raycast(ray, out hit);
            if(Physics.Raycast(ray, out hit)) {
                pm.MouseDestinationVec = hit.point;
            }
        }
    }
}
