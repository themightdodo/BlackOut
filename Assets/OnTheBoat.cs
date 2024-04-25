using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTheBoat : MonoBehaviour
{
    public Vector3 PosOnBoat;
    public GameObject Boat;

    private void Start()
    {
        Boat = Invest_GameManager.GM_instance.GetComponent<BoatComponents>().Boat;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Boat.transform.position + Boat.transform.rotation *PosOnBoat, 0.5f);
    }
}
