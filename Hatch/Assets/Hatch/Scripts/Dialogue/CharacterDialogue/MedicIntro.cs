using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MedicIntro : MonoBehaviour
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

    public void StartDialogue(GameObject dialogueTarget, bool isStatic = false)
    {
        conversationEnsues = true;
        conversationCount = 0;
        if (!GameStates.States[GameStates.MEDIC])
        {
            //StaticEvent.StartDialogue(dialogueTarget, true);
            manager.StartDialogueEvent(dialogueTarget, true);
            manager.StartDialogue(objectiveDialogue[0]);
        }
        else
        {
            //StaticEvent.StartDialogue(dialogueTarget, true);
            manager.StartDialogueEvent(dialogueTarget, true);
            manager.StartDialogue(completedDialogue[0]);
        }
    }

    public void NextDialogue()
    {
        if (conversationEnsues)
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

    public void EndDialogue(bool isStatic = false)
    {
        manager.EndDialogue();
    }
}