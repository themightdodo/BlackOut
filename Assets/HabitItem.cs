using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invest_GameManager.GM_instance.playerManager.GetComponent<Habits>().Add(gameObject);
        Destroy(gameObject);
    }
}
