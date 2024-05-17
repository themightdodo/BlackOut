using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupil : MonoBehaviour
{
    public GameObject pupil;
    Beuverie_GameManager gm;
    Invest_GameManager gmInvest;
    public GameObject Player;
    private void Start()
    {
        gm = Beuverie_GameManager.GM_instance;
        if(gm == null)
        {
            gmInvest = Invest_GameManager.GM_instance;
            Player = gmInvest.playerManager.gameObject;
        }
        else
        {
            Player = gm.playerManager.gameObject;
        }

       
    }
    private void LateUpdate()
    {

        Rotate(pupil,  pupil.transform.position - Player.transform.position );
    }
    public void Rotate(GameObject gameObject, Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(gameObject.transform.forward, direction) * gameObject.transform.rotation;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation, 1000 * Time.deltaTime);

        gameObject.transform.rotation = new Quaternion(0, gameObject.transform.rotation.y, 0, gameObject.transform.rotation.w);
           Quaternion targetUp = Quaternion.FromToRotation(gameObject.transform.up, direction) * gameObject.transform.rotation;
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetUp, 1000 * Time.deltaTime);
    }
}
