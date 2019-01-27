using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneEvent : InteractEvent
{
    public int LoadScene;
    //public Animator SceneFader;

    private void Awake()
    {
        Subscribe();
    }

    public override void TriggerEvent()
    {
        //SceneFader.SetTrigger("fade");
        SceneManager.LoadScene(LoadScene);
    }
}
