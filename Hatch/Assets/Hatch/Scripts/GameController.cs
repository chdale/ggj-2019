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

    internal void InteractEvent()
    {
        Interact();
    }

    internal void EscapeFunctionsEvent()
    {
        CancelDialogue();
    }
}
