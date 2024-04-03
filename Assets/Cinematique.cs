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
    // Start is called before the first frame update
    void Start()
    {
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
        DialogueSound(sentence.Length);
        text.text = sentence;
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

    void DialogueSound(int wordCount)
    {

        if (CurrentDialogue == null)
        {
            return;
        }
        if (CurrentDialogue.PersonTalking != null)
        {
            switch (CurrentDialogue.PersonTalking.genre)
            {
                case Character.Genre.Femme:
                    if (wordCount < 15)
                    {
                        audioManager.Play("FemmeCourt");
                        audioManager.Stop("FemmeLong");
                        audioManager.Stop("FemmeMid");
                    }
                    else if (wordCount > 35)
                    {
                        audioManager.Play("FemmeLong");
                        audioManager.Stop("FemmeCourt");
                        audioManager.Stop("FemmeMid");
                    }
                    else
                    {
                        audioManager.Play("FemmeMid");
                        audioManager.Stop("FemmeCourt");
                        audioManager.Stop("FemmeLong");
                    }
                    break;
                case Character.Genre.Homme:
                    if (wordCount < 15)
                    {
                        audioManager.Play("HommeCourt");
                        audioManager.Stop("HommeLong");
                        audioManager.Stop("HommeMid");
                    }
                    else if (wordCount > 35)
                    {
                        audioManager.Play("HommeLong");
                        audioManager.Stop("HommeCourt");
                        audioManager.Stop("HommeMid");
                    }
                    else
                    {
                        audioManager.Play("HommeMid");
                        audioManager.Stop("HommeCourt");
                        audioManager.Stop("HommeLong");
                    }
                    break;
            }
        }
        else
        {
            if (wordCount < 15)
            {
                audioManager.Play("HommeCourt");
                audioManager.Stop("HommeLong");
                audioManager.Stop("HommeMid");
            }
            else if (wordCount > 35)
            {
                audioManager.Play("HommeLong");
                audioManager.Stop("HommeCourt");
                audioManager.Stop("HommeMid");
            }
            else
            {
                audioManager.Play("HommeMid");
                audioManager.Stop("HommeCourt");
                audioManager.Stop("HommeLong");
            }
        }
    }
    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        /*int i = 1;*/
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
            yield return null;
        }
    }
}
