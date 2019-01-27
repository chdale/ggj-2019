using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchEnter : InteractEvent {

    // Use this for initialization
    private void Awake()
    {
        Subscribe();
    }

    public override void TriggerEvent()
    {
        //Load new level
    }
}
