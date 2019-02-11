using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : TriggeredEvent
{
    public bool isActivated = false;
    public DialogueTarget dialogueTarget;
    private GameController gameController;

    private void Awake()
    {
        Subscribe();
    }

    protected void Subscribe()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameController.Interact += ConditionallyTriggerEvent;
    }

    public override void TriggerEvent()
    {
        isActivated = true;
        StaticEvent.StartDialogue(dialogueTarget);
    }
}
