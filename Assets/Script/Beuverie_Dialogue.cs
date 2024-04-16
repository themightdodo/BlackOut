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

    Beuverie_GameManager gm;

    int InteractCount;

    bool leaveNoEnd;

    AudioManager audioManager;

    InputManager input;

    private void Start()
    {
        sentences = new Queue<string>();
        FindDialogue(Dialogue.startType.Talk, out CurrentDialogue);
        
        gm = Beuverie_GameManager.GM_instance;
        input = gm.GetComponent<InputManager>();
        audioManager = gm.GetComponent<AudioManager>();
    }

    private void Update()
    {
        if(CurrentChatBox == null||CurrentDialogue == null)
        {
            return;
        }
        if (CurrentDialogue.PersonTalking == gm.PlayerInfo)
        {
            CurrentChatBox.transform.position = gm.playerManager.transform.position + new Vector3(0, gm.chatboxHeight, 0);
        }
        if (input.Talk.PressedDown())
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        if (CurrentDialogue == null)
        {
            sentences.Enqueue("...");
        }
        else
        {
           
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            
        }
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        Destroy(CurrentChatBox);
       
        if (sentences.Count == 0&&!leaveNoEnd)
        {
            randomPos = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            EndDialogue();
            return;
        }
      
        string sentence = (string)sentences.Dequeue();
      
        Vector3 RandomPos = transform.position + ChatBoxOffset;
        if (CurrentDialogue != null&&CurrentDialogue.PersonTalking == gm.PlayerInfo)
        {
            RandomPos = gm.playerManager.transform.position + new Vector3(0,gm.chatboxHeight,0);
        }
        CurrentChatBox = Instantiate(Text,RandomPos, transform.rotation);
        CurrentChatBox.GetComponentInChildren<TextMeshPro>().text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    
    void DialogueSound()
    {
        int rand = Random.Range(1, 6);

        switch (rand) 
        {
            case (1):
                audioManager.Play("Bip");
                break;
            case (2):
                audioManager.Play("Bip2");
                break;
            case (3):
                audioManager.Play("Bip3");
                break;
            case (4):
                audioManager.Play("Bip4");
                break;
            case (5):
                audioManager.Play("Bip5");
                break;
            case (6):
                audioManager.Play("Bip6");
                break;
        }
        
    }

    void EndDialogue()
    {
        if (leaveNoEnd)
        {
            return;
        }
        if(CurrentDialogue != null && CurrentDialogue.choices.Count > 0)
        {
            Choix choix = CurrentDialogue.choices[0];
            CurrentDialogue = (Dialogue)CurrentDialogue.GetOutputPort("choices" + " " + 0).Connection.node;
            StartDialogue(CurrentDialogue);
        }
        else
        {
            InteractCount++;
            if(InteractCount > 0)
            {
                gm.CurrentlyTalking = false;
                FindDialogue(Dialogue.startType.Talk2, out CurrentDialogue);
            }
            sentences.Clear();
            launchDialogue = false;
        }
       


    }
    void ResetDialogue()
    {
        Debug.Log("OUT");
        if(CurrentDialogue == null)
        {
            return;
        }
        if(sentences.Count != 0|| CurrentDialogue.choices.Count > 0)
        {
            leaveNoEnd = true;
        }
        else
        {
            CurrentDialogue = null;
            sentences.Clear();
        }
        StopAllCoroutines();
       
        launchDialogue = false;
        gm.CurrentlyTalking = false;
        
       
    }

    IEnumerator TypeSentence(string sentence)
    {
        CurrentChatBox.GetComponentInChildren<TextMeshPro>().text = "";
        string wordBuffer = "";
        bool balise = false;
        int i = 1;
        foreach (char letter in sentence.ToCharArray())
        {
            string l = "";
            l += letter;
            if (l == "<")
            {
                balise = true;

            }
            else if (l == ">")
            {
                balise = false;
            }
            if (balise)
            {
                wordBuffer += letter;
            }
            else
            {
                DialogueSound();
                
                CurrentChatBox.GetComponentInChildren<TextMeshPro>().text += wordBuffer + letter;
                wordBuffer = "";
            }
            
/*            if (i == sentence.ToCharArray().Length)
            {
               Invoke("DisplayNextSentence",gm.TimeBtwDialogues);
            }*/
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
        if (other.CompareTag("Player")&&!launchDialogue&&!gm.CurrentlyTalking)
        {
            Debug.Log(InteractCount);
            if(InteractCount == 0&&!leaveNoEnd)
            {
                FindDialogue(Dialogue.startType.Talk, out CurrentDialogue);
                StartDialogue(CurrentDialogue);
                launchDialogue = true;
                gm.CurrentlyTalking = true;
                leaveNoEnd = false;
            }
            else if(leaveNoEnd)
            {
                launchDialogue = true;
                gm.CurrentlyTalking = true;
                StartDialogue(CurrentDialogue);
                leaveNoEnd = false;

            }


        }       
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            if(CurrentChatBox != null)
            {
                Destroy(CurrentChatBox);
            }
            Debug.Log("sortie dialogue");
            ResetDialogue();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position + ChatBoxOffset, 0.5f);
    }

}
