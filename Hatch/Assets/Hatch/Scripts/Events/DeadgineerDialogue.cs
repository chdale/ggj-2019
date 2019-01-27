using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadgineerDialogue : InteractEvent {
    public bool isActivated = false;

    private void Awake()
    {
        Subscribe();
    }

    public override void TriggerEvent()
    {
        isActivated = true;
        dialogueTargetClass.dialogueTargetName = DialogueTarget.Engineer;
        base.TriggerEvent();
    }
}
