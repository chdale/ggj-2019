using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public DialogueTargetClass dialogueTarget;
    
    private Camera m_camera;
    private GameObject dialogue;
    private bool dialogueActive;
    private GameObject dialogueTargetObject;
    private float cameraLeftLimit = -20.5f;
    private float cameraRightLimit = 20.5f;
    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        m_camera = GetComponent<Camera>();
        dialogue = transform.GetChild(0).GetChild(0).gameObject;
        InteractEvent.StartDialogue += BeginDialogue;
        GameController.CancelDialogue += EndDialogue;
        GameController.EndDialogue += EndDialogue;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!dialogueActive && WithinBounds() && !scene.name.Contains("Console"))
        {
            Vector3 transition = Vector3.Lerp(transform.position, player.transform.position, 5.0f * Time.deltaTime);
            transform.position = new Vector3(transition.x, transform.position.y, -10f);
        }
    }

    private void BeginDialogue()
    {
        dialogueTargetObject = GameObject.Find(dialogueTarget.dialogueTargetName.GetDescription());
        dialogueActive = true;
        dialogue.SetActive(true);
        m_camera.orthographicSize = 7.0f;
        transform.position = new Vector3(MidPointBetween(player, dialogueTargetObject), -4.0f, -10f);
        dialogueTargetObject.GetComponent<FacePlayer>().FaceAndUnfacePlayer(player);
    }

    private void EndDialogue()
    {
        dialogueActive = false;
        dialogue.SetActive(false);
        m_camera.orthographicSize = 10.0f;
        transform.position = new Vector3(player.transform.position.x, 0.0f, -10f);
        dialogueTargetObject.GetComponent<FacePlayer>().FaceAndUnfacePlayer(player);
    }

    private float MidPointBetween(GameObject player, GameObject target)
    {
        return (player.transform.position.x + target.transform.position.x) / 2;
    }

    private bool WithinBounds()
    {
        return player.transform.position.x > cameraLeftLimit && player.transform.position.x < cameraRightLimit;
    }
}
