using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoPickup : InteractEvent {

    public GameObject photo;
    public GameObject fogWall;
    public GameObject overworldPhoto;
    private PhotoManager photoManager;

	// Use this for initialization
	void Start () {
        Subscribe();
        photoManager = GameObject.Find("PhotoManager").GetComponent<PhotoManager>();
	}

    public override void TriggerEvent()
    {
        overworldPhoto.SetActive(false);
        gameController.StopCharacter();
        gameController.InteractInactiveEvent();
        photoManager.FadePhotoIn(photo, fogWall);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
