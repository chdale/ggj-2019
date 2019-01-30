using UnityEngine;

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

        if (levelMusic != null)
        {
            ResetSceneMusic();
        }
        //SceneFader.SetTrigger("fade");
        //SceneManager.LoadScene(LoadScene);
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
