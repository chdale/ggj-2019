using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public DialogueTargetClass dialogueTarget;

    private GameController gameController;
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
    private bool disableStandardCameraControls = false;
    private float lerpDuration = 1.5f;

    private Vector3 fogWallLerpStartingPosition;
    private float fogWallFadeDuration = 3.0f;
    private GameObject fogWall;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        dynamicCameraHorizontal = true;
        cameraLeftLimit = -20.5f;
        cameraRightLimit = 20.5f;
        savedSize = 10.0f;
        level = Level.Hatch;
        m_camera = GetComponent<Camera>();
        dialogue = transform.GetChild(0).GetChild(0).gameObject;
        GameController.StartDialogue += BeginDialogue;
        GameController.CancelDialogue += EndDialogue;
        GameController.EndDialogue += EndDialogue;
        GameController.ClearFogWall += ClearFogWall;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!disableStandardCameraControls)
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
        else
        {
            if (transform.position.x == fogWall.transform.position.x && transform.position.y == fogWall.transform.position.y)
            {
                StartCoroutine(FogWallCoroutine(player, lerpDuration));
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
                m_camera.orthographicSize = 5.0f;
            }
            float dialogueCameraPosition = defaultCameraPosition.y - (savedSize * (2.0f / 5.0f));
            transform.position = new Vector3(MidPointBetween(player, dialogueTargetObject), dialogueCameraPosition, -10f);
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
            if (dynamicCameraHorizontal)
            {
                transform.position = new Vector3(PostEventCameraPosition(), 0, -10);
            }
            else
            {
                transform.position = defaultCameraPosition;
            }
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

    private float PostEventCameraPosition()
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

    private void ClearFogWall(GameObject target)
    {
        disableStandardCameraControls = true;
        fogWall = target;
        fogWallLerpStartingPosition = transform.position;
        StartCoroutine(FogWallCoroutine(target, lerpDuration));
    }

    private IEnumerator FogWallCoroutine(GameObject target, float lerpDuration)
    {
        float targetPositionX;
        float targetPositionY;
        if (target == player)
        {
            targetPositionX = fogWallLerpStartingPosition.x;
            targetPositionY = fogWallLerpStartingPosition.y;
            yield return new WaitForSeconds(fogWallFadeDuration);
        }
        else
        {
            targetPositionX = target.transform.position.x;
            targetPositionY = target.transform.position.y;
        }

        float currentDuration = 0.0f;
        Vector3 startingPosition = transform.position;
        while (currentDuration < 1.0f)
        {
            currentDuration += Time.deltaTime * (Time.timeScale / lerpDuration);
            transform.position = Vector3.Lerp(startingPosition, new Vector3 (targetPositionX, targetPositionY, -10.0f), currentDuration);
            yield return 0;
        }

        if (target == player)
        {
            disableStandardCameraControls = false;
            if (transform.position.x == targetPositionX && transform.position.y == targetPositionY)
            {
                gameController.StartCharacter();
            }
        }
    }
}
