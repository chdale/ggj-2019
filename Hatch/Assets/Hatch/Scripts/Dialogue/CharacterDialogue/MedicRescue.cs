using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MedicRescue : MonoBehaviour
{
    public DialogueManager manager;
    private DialogueObject[] objectiveDialogue;
    private DialogueObject[] completedDialogue;
    private int conversationCount;
    private DialogueEvent medicEvent;
    private bool conversationEnsues = false;
    private AudioSource talkClip;
    private AudioSource playerClip;

    // Use this for initialization
    void Start()
    {
        medicEvent = GameObject.Find("MedicRescueDialogue").GetComponent<DialogueEvent>();
        talkClip = gameObject.GetComponent<AudioSource>();
        playerClip = GameObject.Find("Player_Wireframe").GetComponentInChildren<AudioSource>();
        completedDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.Medic, "Thanks again, see you back at the train!", .04f, Emotions.Idle, talkClip)
        };
        objectiveDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.Medic, "OH MAN, THAT REALLY SMARTS!", .1f, Emotions.Angry, talkClip),
            new DialogueObject(DialogueTarget.Medic, "I guess that's what my time in med school was for, huh?", .04f, Emotions.Idle, talkClip),
            new DialogueObject(DialogueTarget.Player, "Yeah, I guess..", .1f, Emotions.Idle, playerClip),
            new DialogueObject(DialogueTarget.Medic, "No need to let me ramble on...", .1f, Emotions.Idle, talkClip),
            new DialogueObject(DialogueTarget.Medic, "We need to regroup in the train to figure out how to get out of here!", .04f, Emotions.Idle, talkClip)
        };
        GameController.StartDialogue += StartDialogue;
        GameController.NextDialogue += NextDialogue;
        GameController.CancelDialogue += EndDialogue;

    }

    private void StartDialogue(GameObject dialogueTarget, bool isStatic = false)
    {
        if (medicEvent.isActivated)
        {
            conversationEnsues = true;
            conversationCount = 0;
            if (!GameStates.States[GameStates.MEDIC])
            {
                manager.StartDialogue(objectiveDialogue[0]);
            }
            else
            {
                manager.StartDialogue(completedDialogue[0]);
            }
        }
    }

    private void NextDialogue()
    {
        if (medicEvent.isActivated && conversationEnsues)
        {
            if (manager.typeSentenceActive)
            {
                if (!GameStates.States[GameStates.MEDIC])
                {
                    manager.FinishSentence(objectiveDialogue[conversationCount]);
                }
                else
                {
                    manager.FinishSentence(completedDialogue[conversationCount]);
                }
            }
            else
            {
                conversationCount++;
                if (!GameStates.States[GameStates.MEDIC])
                {
                    if (conversationCount > 4)
                    {
                        GameStates.States[GameStates.MEDIC] = true;
                        EndDialogue();
                    }
                    else
                    {
                        manager.DisplayNextSentence(objectiveDialogue[conversationCount]);
                    }
                }
                else
                {
                    if (conversationCount > 0)
                    {
                        EndDialogue();
                    }
                    else
                    {
                        manager.DisplayNextSentence(completedDialogue[conversationCount]);
                    }
                }
            }
        }
    }

    private void EndDialogue(bool isStatic = false)
    {
        medicEvent.isActivated = false;
        manager.EndDialogue();
    }
}