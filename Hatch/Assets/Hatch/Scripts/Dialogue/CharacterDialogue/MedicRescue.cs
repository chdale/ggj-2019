using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicRescue : MonoBehaviour {
    public DialogueManager manager;
    private DialogueObject[] objectiveDialogue;
    private DialogueObject completedDialogue;
    private int conversationCount;
    private MedicRescueDialogueEvent medicEvent;
    private bool conversationEnsues = false;

	// Use this for initialization
	void Start () {
        medicEvent = GameObject.Find("MedicRescueDialogue").GetComponent<MedicRescueDialogueEvent>();
        completedDialogue = new DialogueObject(DialogueTarget.Medic, "Thanks again, see you back at the train!", .04f, Emotions.Idle);
        objectiveDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.Medic, "OH MAN, THAT REALLY SMARTS!", .1f, Emotions.Angry),
            new DialogueObject(DialogueTarget.Medic, "I guess that's what my time in med school was for, huh?", .04f, Emotions.Idle),
            new DialogueObject(DialogueTarget.Player, "Yeah, I guess..", .1f, Emotions.Idle),
            new DialogueObject(DialogueTarget.Medic, "No need to let me ramble on...", .1f, Emotions.Idle),
            new DialogueObject(DialogueTarget.Medic, "We need to regroup in the train to figure out how to get out of here!", .04f, Emotions.Idle)
        };
        InteractEvent.StartDialogue += StartDialogue;
        GameController.NextDialogue += NextDialogue;
        GameController.CancelDialogue += EndDialogue;
    }

    private void StartDialogue()
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
                manager.StartDialogue(completedDialogue);
            }
        }
    }

    private void NextDialogue()
    {
        if (conversationEnsues)
        {
            if (!GameStates.States[GameStates.MEDIC])
            {
                if (conversationCount > 3)
                {
                    GameStates.States[GameStates.MEDIC] = true;
                    EndDialogue();
                }
                else
                {
                    conversationCount++;
                    manager.DisplayNextSentence(objectiveDialogue[conversationCount]);
                }
            }
        }
    }

    private void EndDialogue()
    {
        if (conversationCount > 3)
        {
            GameStates.States[GameStates.MEDIC] = true;
            manager.EndDialogue();
        }
    }
}
