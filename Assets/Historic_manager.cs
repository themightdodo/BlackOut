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

    void Start()
    {
        Data = new Hashtable();
        dialogueBuffer = new List<Dialogue>();
    }


    public void AddToBuffer(Dialogue dialogue)
    {
        dialogueBuffer.Add(dialogue);
    }

    public void SaveHistoric(Character character)
    {
        if (Data.ContainsKey(character))
        {
   
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

    }
    public void CloseHistoricWindow()
    {
        foreach (var item in ShowedText)
        {
            Destroy(item);
        }
        ShowedText.Clear();
    }
}
