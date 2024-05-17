using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using XNode;
using UnityEngine.Events;


public class DialogueManager : Invest_Character_State_Machine
{
    public TextMeshProUGUI text;

    public Chara_dialogue ActiveDialogue;

    public Dialogue CurrentDialogue;

    public Queue<string> sentences;

    public GameObject ButtonPrefab;

    public GameObject ButtonPanel;

    public TextMeshProUGUI PersonTalking;
    public Image ProfilPicture;
    public Image ArthurPicture;
    public Character Arthur;

    public List<GameObject> currentsButtons;

    public List<Choix> ClickedChoix;

    public List<GameObject> Events;

    public Animator animator;

    Historic_manager historic;

    int InteractCount;

    GameObject CurrentButton;
    AudioManager audioManager;
    public Timer ExaminButtonPressTimer;
    float TimeBtwLetters = 0.022f*2;

    bool writing;
    public int InterrogatoireValue;
    string currentsentence;
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
        historic = gm.PhoneManager.historic;
        audioManager = gm.GetComponent<AudioManager>();
        ExaminButtonPressTimer = new Timer(0.25f);
    }


    protected override void LateUpdate()
    {
        base.LateUpdate();

        ExaminButtonPressTimer.Refresh();
        if (pm.Current_Focus_Object != null)
        {
            ActiveDialogue = pm.Current_Focus_Object.GetComponent<Interactible>().chara_Dialogue;
            InteractCount = pm.Current_Focus_Object.GetComponent<Interactible>().interactCount;
            Dialogue item;
            FindDialogue(Dialogue.startType.Examin, out item);
            if (item != null &&item.choixHand.Count > 0 && pm.ItemInHand != null && pm.ItemInHand.GetComponent<Item_Manager>().itemType == item.ItemToHaveInHand)
            {
                foreach (var examintext in gm.CanvasManager.FocusExaminText)
                {
                    examintext.text = item.choixHand[0].text;
                }
            }
            else
            {
                foreach (var examintext in gm.CanvasManager.FocusExaminText)
                {
                    examintext.text = "Examine";
                }
            }
        }
        else if(state_ != State.STATE_PHONE)
        {
            ActiveDialogue = null;
        }
        
        if(dialogue_State == Dialogue_State.STATE_SHOWING&&!gm.GameIsPaused)
        {
            if ((input.Check.PressedDown() || input.Talk.PressedDown())&&ExaminButtonPressTimer.Done())
            {
                if (writing)
                {
                    StopAllCoroutines();
                    text.text = currentsentence;
                    gm.CanvasManager.NextDialogue.SetActive(true);
                    writing = false;
                }
                else
                {
                    DisplayNextSentence();
                }
             
            }
 
        }     
 
    }
    
    public void StartDialogueOut(Chara_dialogue chara_Dialogue,int interactCount,GameObject button)
    {
        ActiveDialogue = chara_Dialogue;
        CurrentButton = button;
        if (interactCount > 0&&!CurrentButton.GetComponent<PhoneInteractible>().Success)
        {
            FindDialogue(Dialogue.startType.Talk2, out CurrentDialogue);
        }
        else if (!CurrentButton.GetComponent<PhoneInteractible>().Success)
        {
            FindDialogue(Dialogue.startType.Talk, out CurrentDialogue);
        }
        else
        {
            FindDialogue(Dialogue.startType.Success, out CurrentDialogue);
        }
        StartDialogue(CurrentDialogue);
        
    }
    protected override void Phone_state()
    {
        gm.DialogueManager.ProfilPicture.gameObject.SetActive(false);
        if (input.Cancel.Pressed())
        {
            state_ = stateBuffer_;
            closeDialogue();
        }
    }
    protected override void TriggerDialogue()
    {
        ActiveDialogue = pm.Current_Focus_Object.GetComponent<Interactible>().chara_Dialogue;
        InteractCount = pm.Current_Focus_Object.GetComponent<Interactible>().interactCount;
        base.TriggerDialogue();
    }
    protected override void Talk_transition()
    {
        if(ActiveDialogue == null)
        {
            return;
        }
        if (input.Talk.PressedDown()||triggerDialogue)
        {
            ExaminButtonPressTimer.Reset();
            if (pm.Current_Focus_Object.GetComponent<Interactible>().Interrogatoire)
            {
                FindDialogue(Dialogue.startType.Interrogatoire, out CurrentDialogue);
                StartDialogue(CurrentDialogue);
                pm.Current_Focus_Object.GetComponent<Interactible>().interactCount++;
                state_ = State.STATE_TALK;
                return;
            }
            else if (InteractCount > 0&& !pm.Current_Focus_Object.GetComponent<Interactible>().Success)
            {
                
                FindDialogue(Dialogue.startType.Talk2, out CurrentDialogue);
                StartDialogue(CurrentDialogue);
                pm.Current_Focus_Object.GetComponent<Interactible>().interactCount++;
                state_ = State.STATE_TALK;
                return;
            }
            else if(!pm.Current_Focus_Object.GetComponent<Interactible>().Success)
            {
                FindDialogue(Dialogue.startType.Talk, out CurrentDialogue);
                StartDialogue(CurrentDialogue);
                pm.Current_Focus_Object.GetComponent<Interactible>().interactCount++;
                state_ = State.STATE_TALK;
                return;
            }
            else
            {
                FindDialogue(Dialogue.startType.Success, out CurrentDialogue);
                StartDialogue(CurrentDialogue);
                pm.Current_Focus_Object.GetComponent<Interactible>().interactCount++;
                state_ = State.STATE_TALK;
                return;
            }
          
           
        }
    }
    protected override void Examin_transition()
    {

        if (input.Check.PressedDown() || triggerDialogue)
        {
            ExaminButtonPressTimer.Reset();
            if (pm.Current_Focus_Object.GetComponent<Interactible>().Interrogatoire)
            {
                Debug.Log("INTERRO");
                FindDialogue(Dialogue.startType.Interrogatoire, out CurrentDialogue);
                pm.Current_Focus_Object.GetComponent<Interactible>().interactCount++;
                StartDialogue(CurrentDialogue);
                state_ = State.STATE_TALK;
                return;
            }
            else
            {
                Debug.Log("EXAMIN");
                Dialogue item;
                FindDialogue(Dialogue.startType.Examin, out item);
                CurrentDialogue = item;
                if(pm.ItemInHand != null && pm.ItemInHand.GetComponent<Item_Manager>().itemType == CurrentDialogue.ItemToHaveInHand && pm.ItemInHand.GetComponent<Item_Manager>().itemType != Item_Manager.ItemType.None)
                {
                    CurrentDialogue = (Dialogue)CurrentDialogue.GetOutputPort("choixHand" + " " + 0).Connection.node;

                }

                StartDialogue(CurrentDialogue);
                state_ = State.STATE_EXAMIN;
            }
         
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
        gm.CanvasManager.NextDialogue.SetActive(false);
  
        gm.DialogueManager.ArthurPicture.gameObject.SetActive(true);
        sentences.Clear();
        clearButtons();

        if (CurrentDialogue == null)
        {
            
            sentences.Enqueue("...");
            
        }
        else
        {
            if (CurrentDialogue.AnimToPlay != "")
            {
                animator.Play(CurrentDialogue.AnimToPlay, -1, 0f);
            }
            else
            {
                animator.Play("Idle");
            }
            if(ActiveDialogue.PersonInteractedWith != null&&state_ != State.STATE_PHONE)
            {
                ProfilPicture.sprite = ActiveDialogue.PersonInteractedWith.ProfilePicture;
                gm.DialogueManager.ProfilPicture.gameObject.SetActive(true);
            }
            else
            {
                gm.DialogueManager.ProfilPicture.gameObject.SetActive(false);
            }
            if (CurrentDialogue.PersonTalking != null)
            {
              
                PersonTalking.text = CurrentDialogue.PersonTalking.name;
                
            }
    
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            if(state_ == State.STATE_PHONE)
            {
                historic.AddToBuffer(CurrentDialogue);
            }
           

        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        TimeBtwLetters = Time.deltaTime * 2;
        gm.CanvasManager.NextDialogue.SetActive(false);
        PersonTalkingColor();
        if (sentences.Count == 0&&CurrentDialogue!=null)
        {       
            GiveSuccess();
            GiveIndice();
            GiveItem();
            SpawnEvent();
            EndDialogue();          
            return;
        }
        else if (sentences.Count == 0&&CurrentDialogue == null)
        {
            Debug.Log(sentences.Count);
            EndDialogue();
            return;
        }


        string sentence = (string)sentences.Dequeue();
        currentsentence = sentence;
        text.text = sentence;
        writing = true;
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
    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        string wordBuffer = "";
        bool balise = false;
        int i = 1;
       
        foreach (char letter in sentence.ToCharArray())
        {
            string l = "";
            l += letter;
            if(l == "<")
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
            else if (l != "[" && l != "]")
            {
                DialogueSound();
                text.text += wordBuffer + letter;
                wordBuffer = "";

            }

            if (l == "[")
            {
                TimeBtwLetters = CurrentDialogue.timeBtwLetter;
            }
            if(l == "]")
            {
                TimeBtwLetters = Time.deltaTime*2;
            }
            l = "";

            if (i == sentence.ToCharArray().Length)
            {
                gm.CanvasManager.NextDialogue.SetActive(true);
                writing = false;
            }
            i++;
            yield return new WaitForSeconds(TimeBtwLetters);
            
        }
        
    }

    void EndDialogue()
    {
        dialogue_State = Dialogue_State.STATE_END;
        if(CurrentDialogue != null && CurrentDialogue.startType_ == Dialogue.startType.Answer)
        {
            CurrentDialogue = (Dialogue)CurrentDialogue.GetOutputPort("choices" + " " + 0).Connection.node;
            StartDialogue(CurrentDialogue);
            return;
        }
        if (CurrentDialogue != null && CurrentDialogue.startType_ == Dialogue.startType.Talk&& CurrentDialogue.choices.Count == 1&&CurrentDialogue.choixIndice.Count == 0)
        {
            Debug.Log("SKIP");
            CurrentDialogue = (Dialogue)CurrentDialogue.GetOutputPort("choices" + " " + 0).Connection.node;
            StartDialogue(CurrentDialogue);
            return;
        }
        /*        if (CurrentDialogue != null && CurrentDialogue.choices.Count == 1)
                {
                    CurrentDialogue = (Dialogue)CurrentDialogue.GetOutputPort("choices" + " " + 0).Connection.node;
                    StartDialogue(CurrentDialogue);
                    return;
                }*/
        if (CurrentDialogue != null &&CurrentDialogue.choices.Count > 0)
        {
            for (int i = 0; i < CurrentDialogue.choices.Count; i++)
            {
                if (!ClickedChoix.Contains(CurrentDialogue.choices[i]))
                {
                    Choix choix = CurrentDialogue.choices[i];
                    var button = Instantiate(ButtonPrefab, ButtonPanel.transform);
                    button.GetComponentInChildren<TextMeshProUGUI>().text = choix.text;
                    button.GetComponent<ChoixButton>().dialogue = (Dialogue)CurrentDialogue.GetOutputPort("choices" + " " + i).Connection.node;
                    button.GetComponent<ChoixButton>().choix = CurrentDialogue.choices[i];
                    currentsButtons.Add(button);
                }

            }

        }
        if (CurrentDialogue != null && pm.ItemInHand != null&& pm.ItemInHand.GetComponent<Item_Manager>().itemType == CurrentDialogue.ItemToHaveInHand)
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
        if (CurrentDialogue != null && pm.GetComponent<Invest_Inventory>().CurrentIndices.Count != 0 && pm.GetComponent<Invest_Inventory>().CurrentIndices.Contains(CurrentDialogue.IndiceToHave2))
        {

                Choix choix = CurrentDialogue.choixIndice[1];
                var button = Instantiate(ButtonPrefab, ButtonPanel.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choix.text;
                button.GetComponent<ChoixButton>().dialogue = (Dialogue)CurrentDialogue.GetOutputPort("choixIndice" + " " + 1).Connection.node;
                currentsButtons.Add(button);
            
        }
        if (CurrentDialogue != null && pm.GetComponent<Invest_Inventory>().CurrentIndices.Count !=0 && pm.GetComponent<Invest_Inventory>().CurrentIndices.Contains(CurrentDialogue.IndiceToHave))
        {

                Choix choix = CurrentDialogue.choixIndice[0];
                var button = Instantiate(ButtonPrefab, ButtonPanel.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choix.text;
                button.GetComponent<ChoixButton>().dialogue = (Dialogue)CurrentDialogue.GetOutputPort("choixIndice" + " " + 0).Connection.node;
                currentsButtons.Add(button);
            
        }
        else if (input.Check.PressedDown() || input.Talk.Pressed())
        {

            if (ActiveDialogue != null && ActiveDialogue.PersonInteractedWith != null && state_ == State.STATE_PHONE)
            {
                historic.SaveHistoric(ActiveDialogue.PersonInteractedWith);
            }
            if (CurrentDialogue != null && pm.Current_Focus_Object != null&&
                CurrentDialogue.choices.Count == 0 && pm.Current_Focus_Object.GetComponent<Interactible>().Interrogatoire)
            {
                
                pm.Current_Focus_Object.GetComponent<Interactible>().Interrogatoire = false;
                if (InterrogatoireValue > -3)
                {
                    FindDialogue(Dialogue.startType.Success, out CurrentDialogue);
                }
                else
                {
                    FindDialogue(Dialogue.startType.Loose, out CurrentDialogue);
                }
                StartDialogue(CurrentDialogue);
                InterrogatoireValue = 0;
            }
            else if(CurrentDialogue != null && pm.ItemInHand != null&& CurrentDialogue.choices.Count == 0
                && pm.ItemInHand.GetComponent<Item_Manager>().itemType != CurrentDialogue.ItemToHaveInHand)
            {
            
                closeDialogue();
            }
            else if (CurrentDialogue != null&& pm.ItemInHand == null && CurrentDialogue.choices.Count == 0)
            {
  
                closeDialogue();
            }
            else if(CurrentDialogue != null && CurrentDialogue.choices.Count == 0)
            {
               
                closeDialogue();
            }
            if (CurrentDialogue == null)
            {
             
                closeDialogue();
            }
           
        }

    }
    void GiveSuccess()
    {
        if (CurrentDialogue.SuccessPoint)
        {
            if(pm.Current_Focus_Object != null &&CurrentButton != null)
            {
                pm.Current_Focus_Object = null;
            }
            if(pm.Current_Focus_Object != null)
            {
                pm.Current_Focus_Object.GetComponent<Interactible>().Success = true;
            }
            if(CurrentButton != null)
            {
                CurrentButton.GetComponent<PhoneInteractible>().Success = true;
            }
        }
    }
    void GiveLoose()
    {
        if (CurrentDialogue.LoosePoint)
        {
            if (pm.Current_Focus_Object != null && CurrentButton != null)
            {
                pm.Current_Focus_Object = null;
            }
            if (pm.Current_Focus_Object != null)
            {
                pm.Current_Focus_Object.GetComponent<Interactible>().Loose = true;
                pm.Current_Focus_Object.GetComponent<Interactible>().interactCount = 2;
            }
            if (CurrentButton != null)
            {
                CurrentButton.GetComponent<PhoneInteractible>().Loose = true;
                pm.Current_Focus_Object.GetComponent<PhoneInteractible>().InteractCount = 2;
            }
        }
    }

    void PersonTalkingColor()
    {
        if(CurrentDialogue == null)
        {
            return;
        }
        if(CurrentDialogue.PersonTalking == Arthur||CurrentDialogue.PersonTalking == null)
        {
            ProfilPicture.color = Color.black;
            ArthurPicture.color = Color.white;
        }
        else
        {
            ProfilPicture.color = Color.white;
            ArthurPicture.color = Color.black;
        }
    }
    void closeDialogue()
    {
        audioManager.Stop("FemmeLong");
        audioManager.Stop("FemmeCourt");
        audioManager.Stop("FemmeMid");
        audioManager.Stop("HommeLong");
        audioManager.Stop("HommeMid");
        audioManager.Stop("HommeCourt");
        sentences.Clear();

        
        CurrentDialogue = null;
        ActiveDialogue = null;
        pm.FinInteraction.Invoke();
        pm.Interaction_cooldown.CurrentValue = pm.Interaction_cooldown.StartValue;
    }
    void GiveIndice()
    {
        if (CurrentDialogue.indicesGiven.Count == 0)
        {
            return;
        }
        pm.AddItemToInventory.Invoke(CurrentDialogue.indicesGiven[0].item);
        if (CurrentDialogue.indicesGiven[0].item.NextObjective)
        {
            gm.CanvasManager.objectifs.NextObjectif(1);
        }
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
    void SpawnEvent()
    {
        if(CurrentDialogue.EventToCreate == null||Events.Contains(CurrentDialogue.EventToCreate))
        {
            return;
        }
        Instantiate(CurrentDialogue.EventToCreate);
        if (!CurrentDialogue.unlimitedEvent)
        {
            Events.Add(CurrentDialogue.EventToCreate);
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
