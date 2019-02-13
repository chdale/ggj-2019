using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEvent
{
    public static void LoadLevel(LevelRequirement levelReq)
    {
        Camera.main.GetComponent<CameraController>().LoadLevel(levelReq);
    }

    public static void StartDialogue(GameObject dialogueTarget = null)
    {
        GameObject.Find("GameController").GetComponent<GameController>().StartDialogueEvent(dialogueTarget);
    }
}
