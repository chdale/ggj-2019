using Assets.Hatch.Scripts.Enumerations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoPickup : TriggeredEvent {

    public GameObject photo;
    public GameObject fogWall;
    public GameObject overworldPhoto;
    public AudioSource photoMusic;

    private List<AudioSource> activeMusic;
    private PhotoManager photoManager;
    private GameController gameController;
    private bool active = false;

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
        activeMusic = new List<AudioSource>();
    }

    public override void TriggerEvent()
    {
        active = true;
        overworldPhoto.SetActive(false);
        gameController.StopCharacter();
        gameController.InteractInactiveEvent();
        MusicFade();
        photoManager.FadePhotoIn(photo, fogWall);
        base.TriggerEvent();
    }

    private void MusicFade()
    {
        var allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (allAudioSources != null)
        {
            foreach (AudioSource audioSource in allAudioSources)
            {
                if (audioSource.isPlaying)
                {
                    activeMusic.Add(audioSource);
                    Fade(false, audioSource);
                    //audioSource.Stop();
                }
            }
        }
        photoMusic.volume = 0f;
        photoMusic.Play();
        Fade(true, photoMusic);
    }

    public void ReturnActiveMusic()
    {
        if (active)
        {
            //photoMusic.Stop();
            Fade(false, photoMusic);
            foreach (AudioSource audioSource in activeMusic)
            {
                //audioSource.Play();
                Fade(true, audioSource);
            }
        }
    }

    private void Fade(bool fadeIn, AudioSource source, float duration = .5f)
    {
        StartCoroutine(FadeAudioCoroutine(fadeIn, source, duration));
    }

    private IEnumerator FadeAudioCoroutine(bool fadeIn, AudioSource source, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (fadeIn)
            {
                source.volume += (Time.deltaTime * 2f);
            }
            else
            {
                source.volume -= (Time.deltaTime * 2f);
            }
        }

        if (source == photoMusic && !fadeIn)
        {
            source.Stop();
        }

        yield return null;
    }
}
