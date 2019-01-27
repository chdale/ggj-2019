using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void InteractAction();
    public static event InteractAction Interact;
    public delegate void CancelDialogueAction();
    public static event CancelDialogueAction CancelDialogue;
    public delegate void FinishKeypadAction();
    public static event FinishKeypadAction FinishKeypad;
    public delegate void EndDialogueAction();
    public static event EndDialogueAction EndDialogue;
    public delegate void NextDialogueAction();
    public static event NextDialogueAction NextDialogue;
    public delegate void CancelJumpAction();
    public static event CancelJumpAction CancelJump;

    internal void InteractEvent()
    {
        Interact();
    }

    internal void EscapeFunctionsEvent()
    {
        CancelDialogue();
    }

    internal void FinishKeypadEvent()
    {
        FinishKeypad();
    internal void EndDialogueEvent()
    {
        EndDialogue();
    }

    internal void NextDialogueEvent()
    {
        NextDialogue();
    }

    internal void CancelJumpEvent()
    {
        CancelJump();
    }
}
