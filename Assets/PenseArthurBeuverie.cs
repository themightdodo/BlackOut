using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PenseArthurBeuverie : MonoBehaviour
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

    private void Start()
    {
        sentences = new Queue<string>();
        FindDialogue(Dialogue.startType.Talk, out CurrentDialogue);

        gm = Beuverie_GameManager.GM_instance;
    }

    private void Update()
    {
        if (CurrentChatBox == null || CurrentDialogue == null)
        {
            return;
        }
        if (CurrentDialogue.PersonTalking == gm.PlayerInfo)
        {
            CurrentChatBox.transform.position = gm.playerManager.transform.position + ChatBoxOffset;
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

        if (sentences.Count == 0 && !leaveNoEnd)
        {
            randomPos = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            EndDialogue();
            return;
        }

        string sentence = (string)sentences.Dequeue();
        Vector3 RandomPos = transform.position + ChatBoxOffset + randomPos;
        if (CurrentDialogue != null && CurrentDialogue.PersonTalking == gm.PlayerInfo)
        {
            RandomPos = gm.playerManager.transform.position + ChatBoxOffset;
        }
        CurrentChatBox = Instantiate(Text, RandomPos, transform.rotation);
        CurrentChatBox.GetComponentInChildren<TextMeshPro>().text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        if (leaveNoEnd)
        {
            return;
        }
        if (CurrentDialogue != null && CurrentDialogue.choices.Count > 0)
        {
            Choix choix = CurrentDialogue.choices[0];
            CurrentDialogue = (Dialogue)CurrentDialogue.GetOutputPort("choices" + " " + 0).Connection.node;
            StartDialogue(CurrentDialogue);
        }
        else
        {
            InteractCount++;
            if (InteractCount > 0)
            {
                FindDialogue(Dialogue.startType.Talk2, out CurrentDialogue);
            }
            sentences.Clear();
            launchDialogue = false;
        }

    }
    void ResetDialogue()
    {
        Debug.Log("OUT");
        if (CurrentDialogue == null)
        {
            return;
        }
        if (sentences.Count != 0 || CurrentDialogue.choices.Count > 0)
        {
            leaveNoEnd = true;
        }
        StopAllCoroutines();
        sentences.Clear();
        launchDialogue = false;
        CurrentDialogue = null;
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
                Invoke("DisplayNextSentence", gm.TimeBtwDialogues);
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
        if (other.CompareTag("Player") && !launchDialogue)
        {
            Debug.Log(InteractCount);
            if (InteractCount == 0)
            {
                FindDialogue(Dialogue.startType.Talk, out CurrentDialogue);
            }
            StartDialogue(CurrentDialogue);
            launchDialogue = true;
            leaveNoEnd = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position + ChatBoxOffset, 0.5f);
    }
}
