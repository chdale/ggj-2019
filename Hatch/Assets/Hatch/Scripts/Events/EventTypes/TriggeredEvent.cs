using UnityEngine;

public class TriggeredEvent : MonoBehaviour
{
    public bool interactable = true;
    public bool oneTimeOnlyEvent = false;
    internal bool triggerable = false;

    public virtual void ConditionallyTriggerEvent()
    {
        if (interactable && triggerable)
        {
            TriggerEvent();
        }
    }

    public virtual void TriggerEvent()
    {
        if (oneTimeOnlyEvent)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!interactable)
            {
                TriggerEvent();
            }
            else
            {
                triggerable = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (interactable)
            {
                triggerable = false;
            }
        }
    }
}