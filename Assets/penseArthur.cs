using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class penseArthur : Invest_Character_State_Machine
{
    public Chara_dialogue chara_Dialogue;

    public Dialogue CurrentDialogue;

    public Queue<string> sentences;

    bool launchDialogue;

    Invest_GameManager gm;

    CanvasManager canvasManager;

    int InteractCount;

    bool leaveNoEnd;

    public float TimeBtwDialogues;

    public TextMeshProUGUI text;

    TextMeshProUGUI PersonneQuiParle;

    protected override void Start()
    {
        base.Start();
        sentences = new Queue<string>();
        FindDialogue(Dialogue.startType.Talk, out CurrentDialogue);

        gm = Invest_GameManager.GM_instance;  
        canvasManager = gm.CanvasManager;
        PersonneQuiParle = gm.DialogueManager.PersonTalking;
        canvasManager.DialoguePanel.SetActive(false);
    }



    protected override void Talk_state()
    {
        base.Talk_state();
        ResetDialogue();
        EndDialogue();
    }
    protected override void Examin_state()
    {
        base.Examin_state();
        ResetDialogue();
        EndDialogue();
    }

    public void FindDialogue(Dialogue.startType startType, out Dialogue outitem)
    {
        outitem = null;
        foreach (Dialogue item in chara_Dialogue.nodes)
        {
            if (item.startType_ == startType)
            {
                outitem = item;
                return;
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        PersonneQuiParle.text = "Arthur";
        if (CurrentDialogue == null)
        {
            sentences.Enqueue("...");
        }
        else
        {

            foreach (string sentence in dialogue.sentences)
            {
                Debug.Log("OUI");
                sentences.Enqueue(sentence);
            }


        }
        canvasManager.thinking = true;
        canvasManager.DialoguePanel.SetActive(true);
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {


        if (sentences.Count == 0 && !leaveNoEnd)
        {

            EndDialogue();
            return;
        }

        string sentence = (string)sentences.Dequeue();
        text.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {

        canvasManager.thinking = false;
        canvasManager.DialoguePanel.SetActive(false);
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
        int i = 1;
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            if (i == sentence.ToCharArray().Length)
            {
                Invoke("DisplayNextSentence", TimeBtwDialogues);
            }
            i++;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(InteractCount > 0)
        {
            return;
        }
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
/*    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            ResetDialogue();
            EndDialogue();
        }
    }*/
}
