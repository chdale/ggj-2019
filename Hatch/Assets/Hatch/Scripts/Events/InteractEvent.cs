using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class InteractEvent : MonoBehaviour, IEnteredEvent
{
    public delegate void StartDialogueAction();
    public static event StartDialogueAction StartDialogue;
    public DialogueTargetClass dialogueTargetClass;

    public GameEventManager manager;
    private Platformer2DUserControl user;
    private bool interactable = false;

    private void Awake()
    {
        user = manager.player.GetComponent<Platformer2DUserControl>();
    }

    protected void Subscribe()
    {
        GameController.Interact += ConditionallyTriggerEvent;
    }

    public virtual void TriggerEvent()
    {
        StartDialogue();
    }

    private void ConditionallyTriggerEvent()
    {
        if (interactable)
        {
            TriggerEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactable = true;
            manager.EnteredEvent();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactable = false;
            manager.ExitedEvent();
        }
    }
}
