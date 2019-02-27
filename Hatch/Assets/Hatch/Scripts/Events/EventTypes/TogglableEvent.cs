using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class TogglableEvent : MonoBehaviour, ITogglableEvent
{
    public GameObject AnimationObject;
    public bool IsTriggered;

    private BoxCollider2D Trigger;

    private void Start()
    {
        Trigger = GetComponent<BoxCollider2D>();
    }

    public virtual void EnterEvent()
    {

    }
    public virtual void LeaveEvent()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnterEvent();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LeaveEvent();
        }
    }
}
