using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public GameObject dialogueTarget;
    public GameObject sceneFader;

    private GameController gameController;
    private Vector3 defaultCameraPosition;
    private bool dynamicCameraHorizontal;
    private Camera m_camera;
    private GameObject dialogue;
    private bool dialogueActive;
    private float cameraLeftLimit;
    private float cameraRightLimit;
    private Level level;
    private float savedSize;
    private bool disableStandardCameraControls = false;
    private float lerpDuration = 1.5f;

    private Vector3 fogWallLerpStartingPosition;
    private float fogWallFadeDuration = 3.0f;
    private GameObject fogWall;
    private PhotoPickup[] photoPickups;

    private void Start()
    {
        photoPickups = FindObjectsOfType<PhotoPickup>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        dynamicCameraHorizontal = false;
        //cameraLeftLimit = -20.5f;
        //cameraRightLimit = 20.5f;
        savedSize = 10.0f;
        level = Level.HatchInterior;
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

    public void SetCamera(LevelRequirement levelRequirement)
    {
        //player.transform.position = levelRequirement.playerPosition;
        defaultCameraPosition = levelRequirement.defaultCameraPosition;
        transform.position = defaultCameraPosition;
        //level = levelRequirement.level;
        //dynamicCameraHorizontal = levelRequirement.dynamicCameraHorizontal;

        //if (dynamicCameraHorizontal)
        //{
        //    cameraLeftLimit = levelRequirement.cameraLeftThreshold;
        //    cameraRightLimit = levelRequirement.cameraRightThreshold;
        //}
        savedSize = levelRequirement.cameraSize;
        m_camera.orthographicSize = savedSize;
    }
    public SceneFader GetSceneFader()
    {
        return sceneFader.GetComponent<SceneFader>();
    }

    public void BDialogue(GameObject dialogueTarget, bool isStatic = false)
    {
        BeginDialogue(dialogueTarget, isStatic);
    }
    public void EDialogue(bool isStatic = false)
    {
        EndDialogue(isStatic);
    }

    private void BeginDialogue(GameObject dialogueTarget, bool isStatic = false)
    {
        if (!dialogueActive)
        {
            dialogueActive = true;
            dialogue.SetActive(true);
            if (!isStatic)
            {
                m_camera.orthographicSize = 5.0f;
                float dialogueCameraPosition = defaultCameraPosition.y - (savedSize * (2.0f / 5.0f));
                if (dialogueTarget != null)
                {
                    transform.position = new Vector3(MidPointBetween(player, dialogueTarget), dialogueCameraPosition, -10f);
                    FacePlayer faceScript = dialogueTarget.GetComponent<FacePlayer>();
                    if (faceScript != null)
                    {
                        faceScript.FaceAndUnfacePlayer(player);
                    }
                }
            }
        }
    }

    private void EndDialogue(bool isStatic = false)
    {
        if (dialogueActive)
        {
            dialogueActive = false;
            dialogue.SetActive(false);
            if (!isStatic)
            {
                m_camera.orthographicSize = savedSize;
                if (dynamicCameraHorizontal)
                {
                    transform.position = new Vector3(PostEventCameraPosition(), 0, -10);
                }
                else
                {
                    transform.position = defaultCameraPosition;
                }

                if (dialogueTarget != null)
                {
                    FacePlayer faceScript = dialogueTarget.GetComponent<FacePlayer>();
                    if (faceScript != null)
                    {
                        faceScript.FaceAndUnfacePlayer(player);
                    }
                }
            }
            else
            {
                transform.position = defaultCameraPosition;
            }

            if (dialogueTarget != null)
            {
                FacePlayer faceScript = dialogueTarget.GetComponent<FacePlayer>();
                if (faceScript != null)
                {
                    faceScript.FaceAndUnfacePlayer(player);
                }
            }

            dialogueTarget = null;
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
        FogController fogWallController = null;
        if (fogWall != null)
        {
            fogWallController = fogWall.GetComponent<FogController>();
        }

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

        if (fogWallController.gameObject.activeSelf)
        {
            fogWallController.ClearFogWall();
        }

        if (target == player)
        {
            disableStandardCameraControls = false;
            if (transform.position.x == targetPositionX && transform.position.y == targetPositionY)
            {
                foreach (var photoPickup in photoPickups)
                {
                    photoPickup.ReturnActiveMusic();
                }
                gameController.StartCharacter();
            }
        }
    }

    public void CameraShakeStart(float duration, float magnitiude)
    {
        this.StartCoroutine(CameraShake(duration, magnitiude));
    }

    public IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public void CameraLerpStart(Vector3 startPos, Vector3 endPos, float duration)
    {
        this.StartCoroutine(CameraLerp(startPos, endPos, duration));
    }

    public IEnumerator CameraLerp(Vector3 startPos, Vector3 endPos, float duration)
    {
        var pitchCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 90.0f);
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            var t = pitchCurve.Evaluate(elapsed / duration);
            t = t / (pitchCurve.Evaluate(1));
            transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

}
