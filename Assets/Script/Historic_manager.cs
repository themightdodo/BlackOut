using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Historic_manager : MonoBehaviour
{
    public Hashtable Data;
    List<Dialogue> dialogueBuffer;
    public GameObject HistoricContainer;
    public GameObject HistoricQuestion;
    public GameObject HistoricAnswer;
    public List<GameObject> ShowedText;
    public TextMeshProUGUI CurrentProfilText;
    public List<Chara_dialogue> MessagesVocales;
    PhoneManager phoneManager;



    void Start()
    {
        phoneManager = Invest_GameManager.GM_instance.PhoneManager;
        Data = new Hashtable();
        dialogueBuffer = new List<Dialogue>();
        foreach (Chara_dialogue item in MessagesVocales)
        {

                FindDialogue(Dialogue.startType.Talk, item, out Dialogue dialogue);
                while (dialogue.choices.Count != 0)
                {
                    AddToBuffer(dialogue);
                    Dialogue CurrentDialogue = (Dialogue)dialogue.GetOutputPort("choices" + " " + 0).Connection.node;
                    dialogue = CurrentDialogue;
                }
            Debug.Log(item.PersonInteractedWith);
            SaveHistoric(item.PersonInteractedWith);
                phoneManager.notification_Manager.AddNotif(item.PersonInteractedWith);
            
            
           
        }
    }


    public void AddToBuffer(Dialogue dialogue)
    {
        Debug.Log("HISTORICBUFFER");
        dialogueBuffer.Add(dialogue);
    }

    public void SaveHistoric(Character character)
    {
        Debug.Log("HISTORICSAVE");
        if (Data.ContainsKey(character))
        {
            List<Dialogue> dialoguesExistant = (List<Dialogue>)Data[character];
            foreach (var item in dialogueBuffer)
            {
                if (!dialoguesExistant.Contains(item))
                {
                    dialoguesExistant.Add(item);
                }
            }
            Data.Remove(character);
            Data.Add(character, dialoguesExistant);
            dialogueBuffer.Clear();
            return;
        }

        List<Dialogue> dialogues = new List<Dialogue>();
        foreach (var item in dialogueBuffer)
        {
            dialogues.Add(item);
        }
        Data.Add(character, dialogues);
        dialogueBuffer.Clear();
        

    }

    public void ShowHistoric(Character character)
    {
        Debug.Log("HISTORICSHOW");
        if (!Data.ContainsKey(character))
        {
            return;
        }
        CurrentProfilText.text = character.name;
        List<Dialogue> dialogues = (List<Dialogue>)Data[character];
        Debug.Log(character);
        foreach (Dialogue dialogue in dialogues)
        {
            
            if (dialogue.PersonTalking ==null || dialogue.PersonTalking != character)
            {          
                foreach (string sentence in dialogue.sentences)
                {
                    
                    GameObject TextObject = Instantiate(HistoricAnswer, HistoricContainer.transform);
                    TextObject.GetComponent<HistoricTextInfo>().text.text = sentence;
                    ShowedText.Add(TextObject);
                }          
            }
            if(dialogue.PersonTalking == character)
            {
                foreach (string sentence in dialogue.sentences)
                {
                   
                    GameObject TextObject = Instantiate(HistoricQuestion, HistoricContainer.transform);
                    TextObject.GetComponent<HistoricTextInfo>().text.text = sentence;
                    ShowedText.Add(TextObject);
                }
            }
        }
        phoneManager.notification_Manager.RemoveNotif(character);
    }
    public void CloseHistoricWindow()
    {
        foreach (var item in ShowedText)
        {
            Destroy(item);
        }
        ShowedText.Clear();
    }

    public void FindDialogue(Dialogue.startType startType, Chara_dialogue ActiveDialogue , out Dialogue outitem)
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
}
