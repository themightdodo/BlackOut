using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Beuverie_Dialogue : MonoBehaviour
{

    public GameObject Text;

    public Chara_dialogue ActiveDialogue;

    public Dialogue CurrentDialogue;

    public Vector3 ChatBoxOffset;

    public Queue<string> sentences;

    GameObject CurrentChatBox;
    
    Vector3 randomPos;

    bool launchDialogue;


    private void Start()
    {
        sentences = new Queue<string>();
        FindDialogue(Dialogue.startType.Talk, out CurrentDialogue);
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        Destroy(CurrentChatBox);

        if (sentences.Count == 0)
        {
            randomPos = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            EndDialogue();
            return;
        }
      
        string sentence = (string)sentences.Dequeue();
        
        CurrentChatBox = Instantiate(Text,transform.position + ChatBoxOffset+ randomPos, transform.rotation);
        CurrentChatBox.GetComponentInChildren<TextMeshPro>().text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        if(CurrentDialogue.choices.Count > 0)
        {
            Choix choix = CurrentDialogue.choices[0];
            CurrentDialogue = (Dialogue)CurrentDialogue.GetOutputPort("choices" + " " + 0).Connection.node;
            StartDialogue(CurrentDialogue);
        }
        else
        {
            sentences.Clear();
            CurrentDialogue = null;
            launchDialogue = false;
        }
       
    }
    IEnumerator TypeSentence(string sentence)
    {
        CurrentChatBox.GetComponentInChildren<TextMeshPro>().text = "";
        int i = 1;
        foreach (char letter in sentence.ToCharArray())
        {
            
            CurrentChatBox.GetComponentInChildren<TextMeshPro>().text += letter;
            if (i == sentence.ToCharArray().Length)
            {
               Invoke("DisplayNextSentence",1);
            }
            i++;
            yield return null;
        }
    }
    public void FindDialogue(Dialogue.startType startType, out Dialogue outitem)
    {
        outitem = null;
        foreach (Dialogue item in ActiveDialogue.nodes)
        {
            if (item.startType_ == startType)
            {
                outitem = item;
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!launchDialogue)
        {
            StartDialogue(CurrentDialogue);
            launchDialogue = true;
        }       
    }

}
