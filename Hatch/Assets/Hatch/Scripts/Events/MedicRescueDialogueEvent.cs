using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicRescueDialogueEvent : InteractEvent
{
    public bool isActivated = false;

    private void Awake()
    {
        Subscribe();
    }

    public override void TriggerEvent()
    {
        isActivated = true;
        dialogueTargetClass.dialogueTargetName = DialogueTarget.Medic;
        base.TriggerEvent();
    }
}
