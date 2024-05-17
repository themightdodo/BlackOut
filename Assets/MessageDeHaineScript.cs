using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageDeHaineScript : MonoBehaviour
{
    public float speed;
    float alpha = 3;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Invest_GameManager.GM_instance.playerManager.transform.position);
        alpha -= 0.001f;
        transform.position -= transform.up * (speed*Random.Range(0.1f,0.7f));
        GetComponent<TextMeshProUGUI>().color = new Color(0.001757503f, 1,0,1);
        if(alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
