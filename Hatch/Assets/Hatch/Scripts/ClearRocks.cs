using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRocks : InteractEvent {

    public GameObject AnimationObject;

    public override void TriggerEvent()
    {
        AnimationObject.GetComponent<AnimationController>().TriggerAnimationsToggle();
    }
}
