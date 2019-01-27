using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchEvent : InteractEvent
{
    public override void TriggerEvent()
    {
        Debug.Log("Down the Hatch");
    }
}
