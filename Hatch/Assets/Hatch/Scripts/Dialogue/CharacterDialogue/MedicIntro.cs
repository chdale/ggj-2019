using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MedicIntro : MonoBehaviour
{
    public GameObject target;
    public DialogueManager manager;
    private DialogueObject[] objectiveDialogue;
    private DialogueObject[] completedDialogue;
    private int conversationCount;
    private DialogueEvent medicEvent;
    private bool conversationEnsues = false;
    public AudioSource talkClip;
    public AudioSource playerClip;


    // Use this for initialization
    void Start()
    {
        completedDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.Medic, "Let me know if you find anything out there.", .04f, Emotions.Idle, talkClip)
        };
        objectiveDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.Medic, "Oh hey, you're up!", .1f, Emotions.Idle, talkClip),
            new DialogueObject(DialogueTarget.Medic, "We really had a rough go at it out there...", .04f, Emotions.Idle, talkClip),
            new DialogueObject(DialogueTarget.Player, "Wha- What happened?", .1f, Emotions.Idle, playerClip),
            new DialogueObject(DialogueTarget.Medic, "I'm not sure...", .1f, Emotions.Idle, talkClip),
            new DialogueObject(DialogueTarget.Medic, "There was a loud noise then I woke up with everything in shambles.", .04f, Emotions.Idle, talkClip),
            new DialogueObject(DialogueTarget.Player, "(I don't even remember how I got onto the subway...)", .1f, Emotions.Idle, playerClip),
            new DialogueObject(DialogueTarget.Medic, "I don't even know if anyone else is hurt out there!", .04f, Emotions.Angry, talkClip),
            new DialogueObject(DialogueTarget.Medic, "I can't get all heated up, not in this situation. I need to keep a cool head.", .04f, Emotions.Idle, talkClip),
            new DialogueObject(DialogueTarget.Medic, "I'm Riley by the way, what's your name?", .04f, Emotions.Idle, talkClip),
            new DialogueObject(DialogueTarget.Player, "...", .5f, Emotions.Idle, playerClip),
            new DialogueObject(DialogueTarget.Medic, "You're probably still shaken up by the crash, don't worry about it.", .04f, Emotions.Idle, talkClip),
            new DialogueObject(DialogueTarget.Medic, "I'll see what supplies I can dig out of this rubble, see what you can find out there.", .04f, Emotions.Idle, talkClip)
        };
        GameController.StartDialogue += StartDialogue;
        GameController.NextDialogue += NextDialogue;
        GameController.CancelDialogue += EndDialogue;

    }

    public void StartDialogue(GameObject dialogueTarget, bool isStatic = false)
    {
        if (dialogueTarget == target)
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
                if (conversationCount == 8)
                {
                    GameStates.States[GameStates.MEDICNAME] = true;
                }
                if (!GameStates.States[GameStates.MEDIC])
                {
                    if (conversationCount > objectiveDialogue.Length - 1)
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
        conversationEnsues = false;
        manager.EndDialogue();
    }
}