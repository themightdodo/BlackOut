using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class feedback_Manager : MonoBehaviour
{
    public bool feedback;
    public GameObject BadFeedBackPanel;
    Timer FeedbackValue;
    Material BadFeedBackMat;
    public float StartValue;
    DialogueManager dm;
    private void Start()
    {
        BadFeedBackMat = BadFeedBackPanel.GetComponent<SpriteRenderer>().material;
        FeedbackValue = new Timer(StartValue);
        BadFeedBackMat.SetFloat("_AlphaClip", 1);
        dm = Invest_GameManager.GM_instance.DialogueManager;
    }
    private void Update()
    {
        if(dm==null||dm.CurrentDialogue == null)
        {
            return;
        }
        if (dm.CurrentDialogue.BadDialogue)
        {
            feedback = true;
           
        }
        if (feedback)
        {
            FeedbackValue.RefreshTime(2);
            BadFeedBackMat.SetFloat("_AlphaClip", 1-FeedbackValue.CurrentValue);
        }
        if (FeedbackValue.Done()&&!dm.CurrentDialogue.BadDialogue)
        {
            FeedbackValue.Reset();
            feedback = false;
            BadFeedBackMat.SetFloat("_AlphaClip", 1);
        }

        
    }
    
}
