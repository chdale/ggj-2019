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

    public void InteractEvent()
    {
        if (Interact != null)
        {
            Interact();
        }
    }

    internal void EscapeFunctionsEvent()
    {
        if (CancelDialogue != null)
        {
            CancelDialogue();
        }
    }

    internal void FinishKeypadEvent()
    {
        if (FinishKeypad != null)
        {
            FinishKeypad();
        }
    }

    internal void EndDialogueEvent()
    {
        if (EndDialogue != null)
        {
            EndDialogue();
        }
    }

    internal void NextDialogueEvent()
    {
        if (NextDialogue != null)
        {
            NextDialogue();
        }
    }

    internal void CancelJumpEvent()
    {
        if (CancelJump != null)
        {
            CancelJump();
        }
    }
}
