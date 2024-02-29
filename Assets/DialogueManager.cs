using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using XNode;

public class DialogueManager : Invest_Character_State_Machine
{
    public TextMeshProUGUI text;

    public Chara_dialogue ActiveDialogue;

    public Dialogue CurrentDialogue;

    public Queue<string> sentences;

    public GameObject ButtonPrefab;

    public GameObject ButtonPanel;

    public List<GameObject> currentsButtons;

    public enum Dialogue_State
    {
        STATE_SHOWING,
        STATE_END,
    }
    public Dialogue_State dialogue_State;

    protected override void Start()
    {
        base.Start();
        sentences = new Queue<string>();
    }


    protected override void Update()
    {
        base.Update();
        if (pm.Current_Focus_Object != null)
        {
            ActiveDialogue = pm.Current_Focus_Object.GetComponent<Interactible>().chara_Dialogue;
        }
        
        if(dialogue_State == Dialogue_State.STATE_SHOWING)
        {
            if (input.Check.PressedDown() || input.Talk.PressedDown())
            {
                DisplayNextSentence();
            }
        }     
    }
    

    protected override void Talk_transition()
    {
        if (input.Talk.Pressed())
        {
            Dialogue item;
            FindDialogue(Dialogue.startType.Talk,out item);
            CurrentDialogue = item;
            StartDialogue(CurrentDialogue);

            state_ = State.STATE_TALK;
        }
    }
    protected override void Examin_transition()
    {
        if (input.Check.Pressed())
        {
            Dialogue item;
            FindDialogue(Dialogue.startType.Examin, out item);
            CurrentDialogue = item;
            StartDialogue(CurrentDialogue);
            state_ = State.STATE_EXAMIN;
        }
    }
    public void FindDialogue(Dialogue.startType startType, out Dialogue outitem)
    {
        outitem = null;
        foreach (Dialogue item in ActiveDialogue.nodes)
        {
            if(item.startType_ == startType)
            {
                outitem = item;
                return;
            }
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        dialogue_State = Dialogue_State.STATE_SHOWING;
        sentences.Clear();
        clearButtons();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            GiveIndice();
            GiveItem();
            EndDialogue();
            return;
        }

        string sentence = (string)sentences.Dequeue();
        text.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    
    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogue_State = Dialogue_State.STATE_END;
        if(CurrentDialogue.choices.Count > 0)
        {
            for (int i = 0; i < CurrentDialogue.choices.Count; i++)
            {
                Choix choix = CurrentDialogue.choices[i];
                var button = Instantiate(ButtonPrefab, ButtonPanel.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choix.text;
                button.GetComponent<ChoixButton>().dialogue = (Dialogue)CurrentDialogue.GetOutputPort("choices" + " " + i).Connection.node;
                currentsButtons.Add(button);
            }

        }
        if (pm.ItemInHand != null&& pm.ItemInHand.GetComponent<Item_Manager>().itemType == CurrentDialogue.ItemToHaveInHand)
        {
            for (int i = 0; i < CurrentDialogue.choixHand.Count; i++)
            {
                Choix choix = CurrentDialogue.choixHand[i];
                var button = Instantiate(ButtonPrefab, ButtonPanel.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choix.text;
                button.GetComponent<ChoixButton>().dialogue = (Dialogue)CurrentDialogue.GetOutputPort("choixHand" + " " + i).Connection.node;
                currentsButtons.Add(button);
            }
        }
        else if ((input.Check.PressedDown() || input.Talk.Pressed()) && CurrentDialogue.choices.Count == 0)
        {
            
            sentences.Clear();
            CurrentDialogue = null;
            ActiveDialogue = null;
            pm.FinInteraction.Invoke();
            pm.Interaction_cooldown.CurrentValue = pm.Interaction_cooldown.StartValue;

        }
    }
    void GiveIndice()
    {
        if (CurrentDialogue.indicesGiven.Count == 0)
        {
            return;
        }
        pm.AddItemToInventory.Invoke(CurrentDialogue.indicesGiven[0].item);
    }
    void GiveItem()
    {
        if(CurrentDialogue.GiveItem == null)
        {
            return;
        }
        pm.AddItemToHand.Invoke(CurrentDialogue.GiveItem);
        if (CurrentDialogue.DestroyWhenGive)
        {
            Destroy(pm.Current_Focus_Object);
        }
    }
    void clearButtons()
    {
        foreach (var item in currentsButtons)
        {
            Destroy(item);
        }
        currentsButtons.Clear();
    }
}
