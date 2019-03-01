using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour
{
    public DialogueManager manager;
    private DialogueObject[] objectiveDialogue;
    private DialogueObject[] completedDialogue;
    private int conversationCount;
    public DialogueEvent transmitterEvent;
    private bool conversationEnsues = false;
    public AudioSource talkClip;

    // Use this for initialization
    void Start()
    {
        completedDialogue = new DialogueObject[]
        {
        };
        objectiveDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.Player, "I wonder if anyone knows how to work this transmitter.", 0.04f, Emotions.Idle, talkClip)
        };
        GameController.StartDialogue += StartDialogue;
        GameController.NextDialogue += NextDialogue;
        GameController.CancelDialogue += EndDialogue;
    }

    private void StartDialogue(GameObject dialogueTarget, bool isStatic = false)
    {
        if (transmitterEvent.isActivated)
        {
            conversationEnsues = true;
            conversationCount = 0;
            manager.StartDialogue(objectiveDialogue[0]);
        }
    }

    private void NextDialogue()
    {
        if (transmitterEvent.isActivated && conversationEnsues)
        {
            if (manager.typeSentenceActive)
            {
                manager.FinishSentence(objectiveDialogue[conversationCount]);
            }
            else
            {
                conversationCount++;
                if (conversationCount > objectiveDialogue.Length - 1)
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
        if (conversationCount > objectiveDialogue.Length - 1)
        {
            transmitterEvent.isActivated = false;
            manager.EndDialogue();
        }
    }
}