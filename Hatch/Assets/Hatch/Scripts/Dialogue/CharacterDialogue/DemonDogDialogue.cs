using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DemonDogDialogue : MonoBehaviour {

    public DialogueManager manager;
    private DialogueObject[] objectiveDialogue;
    private DialogueObject[] completedDialogue;
    private int conversationCount;
    private DemonDog demonDog;
    private bool conversationEnsues = false;
    public AudioSource playerClip;


    // Use this for initialization
    void Start()
    {
        demonDog = gameObject.GetComponent<DemonDog>();
        completedDialogue = new DialogueObject[]
        {
        };
        objectiveDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.Player, "I'm not going near that", .05f, Emotions.Angry, playerClip)
        };
        GameController.StartDialogue += StartDialogue;
        GameController.NextDialogue += NextDialogue;
        GameController.CancelDialogue += EndDialogue;

    }

    public void StartDialogue(GameObject dialogueTarget, bool isStatic = false)
    {
        if (demonDog.agro4Activated)
        {
            conversationEnsues = true;
            conversationCount = 0;
            //StaticEvent.StartDialogue(dialogueTarget, true);
            manager.StartDialogueEvent(dialogueTarget, false);
            manager.StartDialogue(objectiveDialogue[0]);
        }
    }

    public void NextDialogue()
    {
        if (conversationEnsues && demonDog.agro4Activated)
        {
            if (manager.typeSentenceActive)
            {
                manager.FinishSentence(objectiveDialogue[conversationCount]);
            }
            else
            {
                conversationCount++;
                GameStates.States[GameStates.DOG] = true;
                EndDialogue();
            }
        }
    }

    public void EndDialogue(bool isStatic = false)
    {
        conversationEnsues = false;
        demonDog.agro4Activated = false;
        manager.EndDialogue();
    }
}
