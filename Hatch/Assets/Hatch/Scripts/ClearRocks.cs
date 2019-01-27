using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRocks : InteractEvent
{

    public GameObject AnimationObject;
    public GameObject DialogueTrigger;

    private void Awake()
    {
        Subscribe();
    }

    public override void TriggerEvent()
    {
        AnimationObject.GetComponent<AnimationController>().TriggerAnimationsToggle();
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(EnableDialogue());
    }

    IEnumerator EnableDialogue()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            DialogueTrigger.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}