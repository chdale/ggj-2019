using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsTrigger : ToggleableEvent
{

    public GameObject AnimationObject;

    public override void EnterEvent()
    {
        Debug.Log("Enter Event");
        AnimationObject.GetComponent<AnimationController>().TriggerAnimationsToggle();
    }
    public override void LeaveEvent()
    {
        Debug.Log("Leave Event");
        AnimationObject.GetComponent<AnimationController>().TriggerAnimationsToggle();
    }
}