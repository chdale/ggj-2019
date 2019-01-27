using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicRescueDialogueEvent : InteractEvent
{
    public override void TriggerEvent()
    {
        dialogueTargetClass.dialogueTargetName = DialogueTarget.Medic;
        base.TriggerEvent();
    }
}
