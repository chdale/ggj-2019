using UnityEngine;

public class SceneFader : MonoBehaviour
{
    private Animator m_Animator;
    public AudioSource sceneLoad;
    public float timeToLoad;

	// Use this for initialization
	void Start ()
	{
	    m_Animator = gameObject.GetComponent<Animator>();
        Invoke("BeginScene", timeToLoad);
	}

    public void BeginScene()
    {
        m_Animator.SetTrigger("fade");
        sceneLoad.Play();
    }
}
