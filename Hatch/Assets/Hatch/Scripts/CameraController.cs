using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public DialogueTargetClass dialogueTarget;

    private Vector3 defaultCameraPosition;
    private bool dynamicCameraHorizontal;
    private Camera m_camera;
    private GameObject dialogue;
    private bool dialogueActive;
    private GameObject dialogueTargetObject;
    private float cameraLeftLimit;
    private float cameraRightLimit;
    private Level level;
    private float savedSize;

    private void Start()
    {
        dynamicCameraHorizontal = true;
        cameraLeftLimit = -20.5f;
        cameraRightLimit = 20.5f;
        savedSize = 10f;
        level = Level.Hatch;
        m_camera = GetComponent<Camera>();
        dialogue = transform.GetChild(0).GetChild(0).gameObject;
        InteractEvent.StartDialogue += BeginDialogue;
        GameController.CancelDialogue += EndDialogue;
        GameController.EndDialogue += EndDialogue;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!dialogueActive && WithinBounds())
        {
            if (dynamicCameraHorizontal)
            {
                Vector3 transition = Vector3.Lerp(transform.position, player.transform.position, 5.0f * Time.deltaTime);
                transform.position = new Vector3(transition.x, transform.position.y, -10f);
            }
        }
    }

    public void LoadLevel(LevelRequirement levelRequirement)
    {
        player.transform.position = levelRequirement.playerPosition;
        defaultCameraPosition = levelRequirement.defaultCameraPosition;
        transform.position = defaultCameraPosition;
        level = levelRequirement.level;
        dynamicCameraHorizontal = levelRequirement.dynamicCameraHorizontal;

        if (dynamicCameraHorizontal)
        {
            cameraLeftLimit = levelRequirement.cameraLeftThreshold;
            cameraRightLimit = levelRequirement.cameraRightThreshold;
        }
        savedSize = levelRequirement.cameraSize;
        m_camera.orthographicSize = savedSize;
    }

    private void BeginDialogue()
    {
        if (!dialogueActive)
        {
            dialogueTargetObject = GameObject.Find(dialogueTarget.dialogueTargetName.ToString());
            dialogueActive = true;
            dialogue.SetActive(true);
            if (dynamicCameraHorizontal)
            {
                m_camera.orthographicSize = 7.0f;
            }
            transform.position = new Vector3(MidPointBetween(player, dialogueTargetObject), defaultCameraPosition.y - (savedSize * (2 / 5)), -10f);
            FacePlayer faceScript = dialogueTargetObject.GetComponent<FacePlayer>();
            if (faceScript != null)
            {
                faceScript.FaceAndUnfacePlayer(player);
            }
        }
    }

    private void EndDialogue()
    {
        if (dialogueActive)
        {
            dialogueActive = false;
            dialogue.SetActive(false);
            m_camera.orthographicSize = savedSize;
            transform.position = defaultCameraPosition != default(Vector3) ? defaultCameraPosition : new Vector3(PostDialogueCameraPosition(), 0, -10);
            FacePlayer faceScript = dialogueTargetObject.GetComponent<FacePlayer>();
            if (faceScript != null)
            {
                faceScript.FaceAndUnfacePlayer(player);
            }
        }
    }

    private float MidPointBetween(GameObject player, GameObject target)
    {
        return (player.transform.position.x + target.transform.position.x) / 2;
    }

    private bool WithinBounds()
    {
        return player.transform.position.x > cameraLeftLimit && player.transform.position.x < cameraRightLimit;
    }

    private float PostDialogueCameraPosition()
    {
        if (player.transform.position.x < cameraLeftLimit)
        {
            return cameraLeftLimit;
        }
        else if (player.transform.position.x > cameraRightLimit)
        {
            return cameraRightLimit;
        }
        else
        {
            return player.transform.position.x;
        }
    }
}
