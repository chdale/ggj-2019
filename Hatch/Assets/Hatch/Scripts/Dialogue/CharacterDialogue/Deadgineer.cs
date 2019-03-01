using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadgineer : MonoBehaviour
{
    public DialogueManager manager;
    private DialogueObject[] objectiveDialogue;
    private DialogueObject[] completedDialogue;
    private int conversationCount;
    public DialogueEvent deadgineerEvent;
    private bool conversationEnsues = false;
    public AudioSource talkClip;
    public AudioSource playerClip;

    // Use this for initialization
    void Start()
    {
        //deadgineerEvent = GameObject.Find("DeadgineerDialogue").GetComponent<DialogueEvent>();
        completedDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.Engineer, "...", 1.0f, Emotions.Idle, talkClip)
        };
        objectiveDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.Player, "...", 0.5f, Emotions.Idle, playerClip),
            new DialogueObject(DialogueTarget.Engineer, "...", 1.0f, Emotions.Idle, talkClip)
        };
        GameController.StartDialogue += StartDialogue;
        GameController.NextDialogue += NextDialogue;
        GameController.CancelDialogue += EndDialogue;
    }

    private void StartDialogue(GameObject dialogueTarget, bool isStatic = false)
    {
        if (deadgineerEvent.isActivated)
        {
            conversationEnsues = true;
            conversationCount = 0;
            manager.StartDialogue(objectiveDialogue[0]);
        }
    }

    private void NextDialogue()
    {
        if (deadgineerEvent.isActivated && conversationEnsues)
        {
            if (manager.typeSentenceActive)
            {
                manager.FinishSentence(objectiveDialogue[conversationCount]);
            }
            else
            {
                conversationCount++;
                if (conversationCount > 1)
                {
                    EndDialogue();
                }
                else
                {
                    manager.DisplayNextSentence(objectiveDialogue[conversationCount]);
                }
            }
        }
    }

    private void EndDialogue(bool isStatic = false)
    {
        conversationEnsues = false;
        if (conversationCount > 1)
        {
            deadgineerEvent.isActivated = false;
            manager.EndDialogue();
        }
    }
}