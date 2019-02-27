using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowsEmitter : MonoBehaviour {

    public int CrowCount;
    public float Speed;
    public float Rate;
    public GameObject Crow;
    public List<Crow> Crows;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CrowsSceneStart(float duration = 20f)
    {
        gameObject.SetActive(true);
        this.StartCoroutine(CrowsStart(duration));
    }

    public IEnumerator CrowsStart(float duration = 20f)
    {
        var pitchCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 90.0f);
        float elapsed = 0.0f;
        float rateElapsed = 0.0f;

        while (elapsed < duration)
        {
            if (Rate < rateElapsed && Crows.Count < CrowCount) {
                var crow = Object.Instantiate(Crow).GetComponent<Crow>();
                var scale = Random.Range(0.03f, 1f);
                crow.transform.position = transform.position;
                crow.transform.localScale *= scale;
                crow.Speed = Random.Range(0.05f, 0.1f) * scale;
                var ySpeed = Random.Range(0.05f, 0.5f);
                var xSpeed = 1 - ySpeed;
                crow.Direction = new Vector3(1 - ySpeed, 1 - xSpeed, 0);
                Crows.Add(crow);
                rateElapsed = 0;
            }

            rateElapsed += Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
    public void CrowsSceneEnd(float duration = 0.8f)
    {
        Crows.ForEach(x => x.transform.gameObject.SetActive(false));
        gameObject.SetActive(false);
    }
}
