using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneEvent : InteractEvent
{
    //public int LoadScene;
    //public Animator SceneFader;

    private void Awake()
    {
        Subscribe();
    }

    public override void TriggerEvent()
    {
        if (dynamicCameraHorizontal)
        {
            Camera.main.GetComponent<CameraController>().LoadLevel(new LevelRequirement(newPlayerPosition, newCameraPosition, level, dynamicCameraHorizontal, cameraLeftThreshold, cameraRightThreshold, cameraSize));
        }
        else
        {
            Camera.main.GetComponent<CameraController>().LoadLevel(new LevelRequirement(newPlayerPosition, newCameraPosition, level, cameraSize));
        }
        //SceneFader.SetTrigger("fade");
        //SceneManager.LoadScene(LoadScene);
    }
}
