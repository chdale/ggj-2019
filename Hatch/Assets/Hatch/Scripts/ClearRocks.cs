using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRocks : TriggeredEvent
{
    public GameObject AnimationObject;
    public GameObject AnimationObject2;
    public GameObject DialogueTrigger;

    private void Awake()
    {
        GameController.Interact += ConditionallyTriggerEvent;
    }

    public override void TriggerEvent()
    {
        //AnimationObject2.GetComponent<AnimationController>().TriggerAnimationsToggle();
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(EnableDialogue());
    }

    IEnumerator EnableDialogue()
    {
        while (true)
        {
            AnimationObject2.GetComponent<AnimationController>().TriggerAnimationsToggle();
            yield return new WaitForSeconds(1);
            AnimationObject.GetComponent<AnimationController>().TriggerAnimationsToggle();
            yield return new WaitForSeconds(3);
            DialogueTrigger.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}