using Assets.Hatch.Scripts.Enumerations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Hatch.Scripts.ScriptableObjects;

public class GameController : MonoBehaviour
{
    public GameState currentGameState = GameState.Menu;
    public GameStateDatabase.GameState_GameStateData gameStateData;

    public delegate void InteractAction();
    public static event InteractAction Interact;
    public static event InteractAction InteractActive;
    public static event InteractAction InteractInactive;
    public delegate void CancelDialogueAction(bool isStatic = false);
    public static event CancelDialogueAction CancelDialogue;
    public delegate void FinishModalAction();
    public static event FinishModalAction FinishModal;
    public delegate void EndDialogueAction(bool isStatic = false);
    public static event EndDialogueAction EndDialogue;
    public delegate void NextDialogueAction();
    public static event NextDialogueAction NextDialogue;
    public delegate void CancelJumpAction();
    public static event CancelJumpAction CancelJump;
    public delegate void StopPlayerAction();
    public static event StopPlayerAction StopPlayer;
    public delegate void StartPlayerAction();
    public static event StartPlayerAction StartPlayer;
    public delegate void StartDialogueAction(GameObject dialogueTarget, bool isStatic = false);
    public static event StartDialogueAction StartDialogue;
    public delegate void CancelPhotoAction();
    public static event CancelPhotoAction CancelPhoto;
    public delegate void ClearFogWallAction(GameObject fogWall);
    public static event ClearFogWallAction ClearFogWall;

    public bool isPhotoActive = false;
    public bool isInDialogue = false;

    private void Start()
    {
        StaticEvent.LoadLevel(gameStateData[currentGameState]);
    }

    public void StopCharacter()
    {
        if (StopPlayer != null)
        {
            StopPlayer();
        }
    }

    public void StartCharacter()
    {
        if (StartPlayer != null)
        {
            StartPlayer();
        }
    }

    public void InteractEvent()
    {
        if (Interact != null)
        {
            Interact();
        }
    }

    public void InteractActiveEvent()
    {
        if (InteractActive != null)
        {
            InteractActive();
        }
    }

    public void InteractInactiveEvent()
    {
        if (InteractInactive != null)
        {
            InteractInactive();
        }
    }

    internal void EscapeFunctionsEvent()
    {
        CancelDialogue();
        CancelPhotoEvent();
        FinishModalEvent();
    }

    public void ClearFogWallEvent(GameObject fogWall)
    {
        if (ClearFogWall != null)
        {
            ClearFogWall(fogWall);
        }
    }

    internal void CancelDialogueEvent()
    {
        if (CancelDialogue != null)
        {
            CancelDialogue();
            InteractActiveEvent();
            StartCharacter();
            isInDialogue = false;
        }
    }

    public void CancelPhotoEvent()
    {
        if (CancelPhoto != null && isPhotoActive)
        {
            CancelPhoto();
        }
    }

    public void FinishModalEvent()
    {
        if (FinishModal != null)
        {
            FinishModal();
            StartCharacter();
        }
    }

    internal void EndDialogueEvent()
    {
        if (EndDialogue != null)
        {
            EndDialogue();
            StartCharacter();
            isInDialogue = false;

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

    public void StartDialogueEvent(GameObject dialogueTarget, bool isStatic = false)
    {
        if (StartDialogue != null)
        {
            StartDialogue(dialogueTarget, isStatic);
            InteractInactiveEvent();
            StopCharacter();
            isInDialogue = true;
        }
    }
    public void EndDialogueEvent(GameObject dialogueTarget, bool isStatic = false)
    {
        if (StartDialogue != null)
        {
            StartDialogue(dialogueTarget, isStatic);
            InteractInactiveEvent();
            StopCharacter();
            isInDialogue = false;
        }
    }
}
