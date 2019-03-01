using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets._2D;

public class LevelLoadEvent : TriggeredEvent
{
    //Level Requirement Items
    public Vector2 newPlayerPosition;
    public Vector3 newCameraPosition;
    public Level level;
    public AudioSource[] levelMusic;
    public AudioSource tasteAudio;
    public bool dynamicCameraHorizontal = false;
    public float cameraLeftThreshold;
    public float cameraRightThreshold;
    public float cameraSize = 10.0f;
    public bool removeTasteAudio = true;

    private void Awake()
    {
        Subscribe();
    }

    protected void Subscribe()
    {
        GameController.Interact += ConditionallyTriggerEvent;
    }

    public override void TriggerEvent()
    {
        if (dynamicCameraHorizontal)
        {
            StaticEvent.LoadLevel(new LevelRequirement(newPlayerPosition, newCameraPosition, level, dynamicCameraHorizontal, cameraLeftThreshold, cameraRightThreshold, cameraSize));
        }
        else
        {
            StaticEvent.LoadLevel(new LevelRequirement(newPlayerPosition, newCameraPosition, level, cameraSize));
        }

        if (levelMusic.Any())
        {
            ResetSceneMusic();
        }
        if (tasteAudio != null)
        {
            if (!removeTasteAudio)
            {
                AddTasteAudio();
            }
            else
            {
                RemoveTasteAudio();
            }
        }
    }

    private void RemoveTasteAudio()
    {
        tasteAudio.Stop();
    }

    private void AddTasteAudio()
    {
        tasteAudio.Play();
    }

    public void ResetSceneMusic()
    {
        var allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (allAudioSources != null)
        {
            foreach (AudioSource audioSource in allAudioSources)
            {
                audioSource.Stop();
            }
        }

        foreach (var music in levelMusic)
        {
            music.Play();
        }
    }
}
