using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticEvent
{
    public static void LoadLevel(LevelRequirement levelReq)
    {
        Camera.main.GetComponent<CameraController>().LoadLevel(levelReq);
    }

    public static void StartDialogue(DialogueTarget dialogueTarget = DialogueTarget.Player)
    {
        GameObject.Find("GameController").GetComponent<GameController>().StartDialogueEvent(dialogueTarget);
    }
}
