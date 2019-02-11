using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class LevelLoadEvent : TriggeredEvent
{
    //Level Requirement Items
    public Vector2 newPlayerPosition;
    public Vector3 newCameraPosition;
    public Level level;
    public AudioSource levelMusic;
    public bool dynamicCameraHorizontal = false;
    public float cameraLeftThreshold;
    public float cameraRightThreshold;
    public float cameraSize = 10.0f;

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

        if (levelMusic != null)
        {
            ResetSceneMusic();
        }
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

        levelMusic.Play();
    }
}
