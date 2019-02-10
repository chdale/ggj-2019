using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoManager : MonoBehaviour {

    private CanvasRenderer photoPanelRenderer;
    private CanvasRenderer photoImageRenderer;
    private GameController gameController;

    public GameObject leavePhotoText;

    [SerializeField]
    private float fadeSpeed = 0.4f;
    private float maxAlpha = 1.0f;
    private float currentPanelAlpha;
    private float currentPhotoAlpha;
    private bool isFadeIn = false;
    private bool isFadeOut = false;
    private float currentFade = 0.0f;
    private GameObject fogWall;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        photoPanelRenderer = transform.GetChild(0).GetChild(0).GetComponent<CanvasRenderer>();
        photoPanelRenderer.SetAlpha(0.0f);
        GameController.CancelPhoto += FadePhotoOut;
    }

    private void Update()
    {
        if (isFadeIn)
        {
            if (currentPhotoAlpha >= maxAlpha && currentPanelAlpha >= maxAlpha)
            {
                isFadeIn = false;
                gameController.isPhotoActive = true;
                StartCoroutine(LeavePhotoText());
            }

            currentFade = (Time.deltaTime * fadeSpeed);

            if (currentPhotoAlpha < maxAlpha)
            {
                photoImageRenderer.SetAlpha(photoImageRenderer.GetAlpha() + currentFade);
                currentPhotoAlpha = photoImageRenderer.GetAlpha();
            }
            if (currentPanelAlpha < maxAlpha)
            {
                photoPanelRenderer.SetAlpha(photoPanelRenderer.GetAlpha() + currentFade);
                currentPanelAlpha = photoPanelRenderer.GetAlpha();
            }
        }

        //if (isFadeOut)
        //{
        //    if (currentPhotoAlpha <= 0.0f && currentPanelAlpha >= 0.0f)
        //    {
        //        isFadeOut = false;
        //        photoImageRenderer.gameObject.SetActive(false);
        //    }

        //    currentFade = (Time.deltaTime * fadeSpeed);

        //    if (currentPhotoAlpha > 0.0f)
        //    {
        //        photoImageRenderer.SetAlpha(photoImageRenderer.GetAlpha() - currentFade);
        //        currentPhotoAlpha = photoImageRenderer.GetAlpha();
        //    }
        //    if (currentPanelAlpha > 0.0f)
        //    {
        //        photoPanelRenderer.SetAlpha(photoPanelRenderer.GetAlpha() - currentFade);
        //        currentPanelAlpha = photoPanelRenderer.GetAlpha();
        //    }
        //}
    }

    public void FadePhotoIn(GameObject photo, GameObject fogWall)
    {
        this.fogWall = fogWall;
        photoImageRenderer = photo.GetComponent<CanvasRenderer>();
        photoImageRenderer.SetAlpha(0.0f);
        photo.SetActive(true);
        currentPanelAlpha = photoPanelRenderer.GetAlpha();
        currentPhotoAlpha = photoImageRenderer.GetAlpha();
        isFadeIn = true;
    }

    public void FadePhotoOut()
    {
        StopAllCoroutines();
        gameController.isPhotoActive = false;
        //currentPanelAlpha = photoPanelRenderer.GetAlpha();
        //currentPhotoAlpha = photoImageRenderer.GetAlpha();
        photoPanelRenderer.SetAlpha(0.0f);
        photoImageRenderer.SetAlpha(0.0f);
        photoImageRenderer.gameObject.SetActive(false);
        leavePhotoText.SetActive(false);
        gameController.ClearFogWallEvent(fogWall);
        //isFadeOut = true;
    }

    private IEnumerator LeavePhotoText()
    {
        if (gameController.isPhotoActive)
        {
            yield return new WaitForSeconds(2.0f);
            leavePhotoText.SetActive(true);
        }
    }
}
