using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(InputManager))]
public class Cinematique : MonoBehaviour
{
    public TextMeshProUGUI text;

    public Chara_dialogue ActiveDialogue;

    public Dialogue CurrentDialogue;

    public Queue<string> sentences;
    public TextMeshProUGUI PersonTalking;

    public string SceneToLoad;

    AudioManager audioManager;
    InputManager input;

    public GameObject NextDialogue;
    bool writing;
    float TimeBtwLetters = 0.022f * 2;
    string currentsentence;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        sentences = new Queue<string>();
        audioManager = GetComponent<AudioManager>();
        input = GetComponent<InputManager>();
        FindDialogue(Dialogue.startType.Talk, out CurrentDialogue);
        StartDialogue(CurrentDialogue);
    }

    private void Update()
    {
        if (input.Check.PressedDown() || input.Talk.PressedDown())
        {
            if (writing)
            {
                StopAllCoroutines();
                text.text = currentsentence;
                NextDialogue.SetActive(true);
                writing = false;
            }
            else
            {
                DisplayNextSentence();
            }
                
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        NextDialogue.SetActive(false);
        if (CurrentDialogue == null)
        {
            sentences.Enqueue("...");
        }
        else
        {
            if (CurrentDialogue.PersonTalking != null)
            {
                PersonTalking.text = CurrentDialogue.PersonTalking.name;
            }
            else
            {
                PersonTalking.text = "Arthur";
            }
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 && CurrentDialogue != null)
        {
            EndDialogue();
            return;
        }
        else if (sentences.Count == 0 && CurrentDialogue == null)
        {

            EndDialogue();
            return;
        }


        string sentence = (string)sentences.Dequeue();
        text.text = sentence;
        currentsentence = text.text;
        writing = true;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue()
    {
        if (CurrentDialogue != null && CurrentDialogue.choices.Count > 0)
        {
            Choix choix = CurrentDialogue.choices[0];
            CurrentDialogue = (Dialogue)CurrentDialogue.GetOutputPort("choices" + " " + 0).Connection.node;
            StartDialogue(CurrentDialogue);
        }
        else if (input.Check.PressedDown() || input.Talk.Pressed())
        {
            if (CurrentDialogue != null && CurrentDialogue.choices.Count == 0)
            {
                SceneManager.LoadScene(SceneToLoad);
            }
            if (CurrentDialogue == null)
            {
                SceneManager.LoadScene(SceneToLoad);
            }
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
        int i = 1;
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            /*            if (i == sentence.ToCharArray().Length)
                        {
                            audioManager.Stop("FemmeLong");
                            audioManager.Stop("FemmeCourt");
                            audioManager.Stop("FemmeMid");
                            audioManager.Stop("HommeLong");
                            audioManager.Stop("HommeMid");
                            audioManager.Stop("HommeCourt");
                        }
                        i++;*/
            DialogueSound();
            if (i == sentence.ToCharArray().Length)
            {
                writing = false;
                NextDialogue.SetActive(true);
            }
            i++;
            yield return new WaitForSeconds(TimeBtwLetters);
        }
    }
}
