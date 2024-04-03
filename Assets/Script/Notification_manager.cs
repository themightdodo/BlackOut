using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification_manager : MonoBehaviour
{
    public List<GameObject> NotificationObject;

    int TotalNotificationCount = 0;
    public List<ProfilManager> profilsNotifs;

    private void Start()
    {
        foreach (var profil in profilsNotifs)
        {
            profil.NotificationObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(TotalNotificationCount > 0)
        {
            foreach (var item in NotificationObject)
            {
                item.GetComponentInChildren<TextMeshProUGUI>().text = "" + TotalNotificationCount;
                item.SetActive(true);
            }
            
        }
        else
        {
            foreach (var item in NotificationObject)
            {
                item.SetActive(false);
            }
        }
    }

    public void RemoveNotif(Character character)
    {
        foreach (var item in profilsNotifs)
        {
            if (item.character == character&& item.NotificationObject.activeSelf)
            {
                item.NotificationObject.SetActive(false);
                TotalNotificationCount -= item.NotificationCount;
            }
        }
    }
    public void AddNotif(Character character)
    {
        foreach (var item in profilsNotifs)
        {
            if(item.character == character)
            {
                item.NotificationObject.SetActive(true);
                TotalNotificationCount += item.NotificationCount;
            }
        }
    }




}
