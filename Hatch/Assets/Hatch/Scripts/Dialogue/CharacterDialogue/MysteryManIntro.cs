using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MysteryManIntro : MonoBehaviour
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

        };
        objectiveDialogue = new DialogueObject[]
        {
            new DialogueObject(DialogueTarget.MysteryMan, "Sup", .1f, Emotions.Idle, talkClip),
            new DialogueObject(DialogueTarget.MysteryMan, "Peace", .04f, Emotions.Idle, talkClip)
        };
    }

    public void StartDialogue(GameObject dialogueTarget)
    {
        conversationEnsues = true;
        conversationCount = 0;
        StaticEvent.StartDialogue(gameObject, true);
        manager.StartDialogue(objectiveDialogue[0]);
    }

    public void NextDialogue()
    {
        conversationCount++;
        manager.DisplayNextSentence(objectiveDialogue[conversationCount]);
    }

    public void EndDialogue()
    {
        manager.EndDialogue();
    }
}