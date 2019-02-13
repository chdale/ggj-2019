using System.Collections;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    private Animator m_Animator;
    private GameController gameController;

    public AudioSource sceneLoad;
    public float timeToLoad;

	// Use this for initialization
	void Start ()
	{
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
	    m_Animator = gameObject.GetComponent<Animator>();
        StartCoroutine(StopPlayerDuration());
        Invoke("BeginScene", timeToLoad);
	}

    public void BeginScene()
    {
        m_Animator.SetTrigger("fade");
        sceneLoad.Play();
    }
    public void SetTrigger(string trigger)
    {
        m_Animator.SetTrigger(trigger);
    }

    IEnumerator StopPlayerDuration()
    {
        gameController.StopCharacter();
        yield return new WaitForSeconds(timeToLoad);
        gameController.StartCharacter();
    }
}
