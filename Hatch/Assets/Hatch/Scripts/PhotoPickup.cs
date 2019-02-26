using Assets.Hatch.Scripts.Enumerations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoPickup : TriggeredEvent {

    public GameObject photo;
    public GameObject fogWall;
    public GameObject overworldPhoto;

    private PhotoManager photoManager;
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        Subscribe();
        if (gameController.currentGameState >= GameState.Photo1)
        {
            overworldPhoto.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Subscribe()
    {
        GameController.Interact += ConditionallyTriggerEvent;
        photoManager = GameObject.Find("PhotoManager").GetComponent<PhotoManager>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public override void TriggerEvent()
    {
        overworldPhoto.SetActive(false);
        gameController.StopCharacter();
        gameController.InteractInactiveEvent();
        photoManager.FadePhotoIn(photo, fogWall);
        base.TriggerEvent();
    }
}
