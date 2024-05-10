using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatComponents : MonoBehaviour
{
    public GameObject BoatCam;
    public GameObject Boat;
    public GameObject PauseMenu;
    public GameObject EndMenuPopup;
    public List<GameObject> gameObjectsOnBoat;
    public List<GameObject> InstantiatedObjects;
    Rigidbody BoatRigidbody;
    GameObject Camera;
    GameObject Player;
    Invest_PlayerManager playerManager;

    public enum BoatState
    {
        NONE,
        BOAT,
    }

    public BoatState boatState;

    private void Start()
    {
        Boat.GetComponent<Rigidbody>().isKinematic = true;
        playerManager = Invest_GameManager.GM_instance.playerManager;
        Camera = Invest_GameManager.GM_instance.playerManager.Camera;
        Player = Invest_GameManager.GM_instance.playerManager.gameObject;
        BoatRigidbody = Boat.GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        if(boatState == BoatState.BOAT&&(GetComponent<InputManager>().Check.PressedDown()||GetComponent<InputManager>().Cancel.PressedDown())&&!PauseMenu.activeSelf&&!EndMenuPopup.activeSelf)
        {
            DeActive();
        }
    }

    public void Active()
    {
        boatState = BoatState.BOAT;
        DestroyObjects();
        BoatRigidbody.isKinematic = false;
        Player.GetComponent<CharacterController>().enabled = false;
        playerManager.MiniJeu.Invoke();
        Camera.SetActive(false);
        BoatCam.SetActive(true);
    }
    public void DeActive()
    {
        boatState = BoatState.NONE;
        InstantiateObjects();
        BoatRigidbody.isKinematic = true;
        Player.GetComponent<CharacterController>().enabled = true;
        playerManager.FinMiniJeu.Invoke();
        Camera.SetActive(true);
        BoatCam.SetActive(false);
    }
    void InstantiateObjects()
    {
        foreach (var item in gameObjectsOnBoat)
        {
            Vector3 pos = Boat.transform.position + Boat.transform.rotation * item.GetComponent<OnTheBoat>().PosOnBoat;
            InstantiatedObjects.Add(Instantiate(item, pos, item.transform.rotation));
        }
    }
    void DestroyObjects()
    {
        foreach (var item in InstantiatedObjects)
        {
            Destroy(item);
        }
    }
}
