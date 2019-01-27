using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterConsoleExternal : InteractEvent {

    private void Awake()
    {
        Subscribe();
    }

    public override void TriggerEvent()
    {
        //Load Level
    }
}
