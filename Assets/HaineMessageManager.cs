using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HaineMessageManager : MonoBehaviour
{
    public List<string> Messages;
    public List<string> InstanciatedMessages;
    public List<GameObject> SpawnedObject;
    public List<Vector3> SpawnPositions;
    public GameObject ChatBox;
    public int TimeBtwSpawn;

    private void Start()
    {
        StartCoroutine(Message());
    }

    IEnumerator Message()
    {
        if(Messages.Count == 0)
        {
            yield return null;
        }
        foreach (var item in Messages)
        {
                ChatBox.GetComponent<TextMeshProUGUI>().text = item;
                Instantiate(ChatBox, transform.position + transform.rotation * SpawnPositions[Random.Range(0,SpawnPositions.Count)],ChatBox.transform.rotation,transform);
            
            yield return new WaitForSeconds(TimeBtwSpawn);
        }
        StartCoroutine(Message());
    }
    public void Add(string message)
    {
        StopAllCoroutines();
        Messages.Add(message);
        StartCoroutine(Message());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var item in SpawnPositions)
        {
            Gizmos.DrawSphere(transform.position + item, 10f);
        }
    }
}
