using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DemonDogDialogue : MonoBehaviour {

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
            new DialogueObject(DialogueTarget.Player, "...", .04f, Emotions.Idle, talkClip)
        };
        objectiveDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.Player, "I'm not going near that", .2f, Emotions.Angry, talkClip)
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
                    if (conversationCount >= 1)
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
