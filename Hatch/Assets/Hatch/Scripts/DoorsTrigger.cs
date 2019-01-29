using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsTrigger : TogglableEvent
{
    public override void EnterEvent()
    {
        AnimationObject.GetComponent<AnimationController>().TriggerAnimationsToggle(true);
    }
    public override void LeaveEvent()
    {
        AnimationObject.GetComponent<AnimationController>().TriggerAnimationsToggle(false);
    }
}