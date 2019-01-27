using System.Collections;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    private Animator m_Animator;
    public AudioSource sceneLoad;
	// Use this for initialization
	void Start ()
	{
	    m_Animator = gameObject.GetComponent<Animator>();
	    StartCoroutine(BeginScene(20f));
	}

    IEnumerator BeginScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_Animator.SetTrigger("fade");
        sceneLoad.Play();
    }
}
