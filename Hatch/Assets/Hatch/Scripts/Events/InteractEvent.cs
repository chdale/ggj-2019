using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class InteractEvent : MonoBehaviour, IEnteredEvent
{
    public delegate void StartDialogueAction();
    public static event StartDialogueAction StartDialogue;
    public DialogueTargetClass dialogueTargetClass;

    private GameEventManager manager;
    private Platformer2DUserControl user;
    private bool interactable = false;

    private void Start()
    {
        manager = transform.parent.GetComponent<GameEventManager>();
        user = manager.player.GetComponent<Platformer2DUserControl>();
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
